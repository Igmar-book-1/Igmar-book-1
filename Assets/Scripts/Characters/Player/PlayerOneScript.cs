using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//TP2 - Matias Sueldo
public class PlayerOneScript : AllCharacterController
{
    protected bool _isJumping = false;
    protected bool _canJump = true;
    protected bool _onAttack = false;
    protected int _comboAttack = 0;
    protected bool _isAttacking = false;
    protected bool _isAiming = false;
    protected bool _isRunning = false;
    protected bool _isGrounded = false;
    protected bool _isDashing = false;
    private bool _isComboSystemActive=false;
    protected int _speedMovementAnimation = 1;
    private int comboTimer = 0;
    protected int mana = 100;
    [SerializeField] protected float jumpForce = 0;
    [SerializeField] protected bool _isMoving = false;
    [SerializeField] protected string onJump = "onJump";
    [SerializeField] protected string onAttack = "onAttack";
    [SerializeField] protected string isAiming = "isAiming";
    [SerializeField] protected string isAttacking = "isAttacking";
    [SerializeField] protected string comboAttack = "comboAttack";
    [SerializeField] protected string onCreateGround = "onCreateGround";
    [SerializeField] protected string onCreateWall = "onCreateWall";
    [SerializeField] protected string onCallBird = "onCallBird";
    [SerializeField] protected string onBlock = "onBlock";
    [SerializeField] protected string onDash = "onDash";
    private EventManager eventManager;
    bool creationCooldown = false;
    private bool isCreatingPlatform = false;
    PlayerOneMovement playerOneMovement;
    private MouseSpawnWallController mouseSpawn;
    private StaffDamageScript staff;
    private Vector3 checkpoint;

    private bool abilityRIsEnabled = false;
    private bool abilityQIsEnabled = false;

    [SerializeField] float BlockECooldown = 5f;
    [SerializeField] float AttackRCooldown = 5f;
    [SerializeField] float DashQCooldown = 5f;
    private float startOfFall;

    private float currentBlockECooldown = 0f;
    private float currentAttackRCooldown = 0f;
    private float currentDashQCooldown = 0f;
    private bool isRefreshingSkills = false;


    private void Start()
    {
        playerOneMovement = new PlayerOneMovement(this._anim, this._rb, this.jumpForce);
        mouseSpawn = new MouseSpawnWallController();
        mouseSpawn.Start();
        staff = GameObject.FindWithTag("Staff").GetComponent<StaffDamageScript>();
        eventManager = GameManager.instance.EventManager.GetComponent<EventManager>();

        checkpoint = this.transform.position;
        updateCoolDownAllSkills();
        StartCoroutine(onStartDontHurt());
        isHurt = true;
        Cursor.lockState = CursorLockMode.Locked;   // keep confined to center of screen
    }

    IEnumerator onStartDontHurt()
    {
        yield return new WaitForSeconds(5);
        isHurt = false;

    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript._isPause)
        {
            if (!IsDead && !isCreatingPlatform)
            {

                base._xAxis = Input.GetAxis("Horizontal");
                base._zAxis = Input.GetAxis("Vertical");
                _isJumping = Input.GetButtonDown("Jump");
                _onAttack = Input.GetButtonDown("Fire1");
                _isAiming = Input.GetButton("Fire2");
                _isRunning = Input.GetButton("Fire3");
                
                base._anim.SetBool(isAiming, _isAiming);

                _anim.SetFloat("verticalSpeed", _rb.velocity.y);

                if (_isJumping && _canJump)
                {
                    _canJump = false;
                    StartCoroutine(playerOneMovement.Jump(_isGrounded));

                }

                if (_onAttack && _isGrounded && !_isAttacking)
                {
                    comboTimer = 3;
                    if(!_isComboSystemActive)
                    {
                        StartCoroutine(ComboSystem());
                    }
                    _isAttacking = true;
                    base._anim.SetTrigger(onAttack);
                    base._anim.SetInteger(comboAttack, _comboAttack);
                    if (_comboAttack < 2)
                    {
                        _comboAttack++;
                        if (_comboAttack == 1)
                        {

                        }
                    }
                    else
                    {
                        _comboAttack = 0;

                    }

                }

                if (Input.GetKeyDown(KeyCode.Q) && currentDashQCooldown ==0 && abilityQIsEnabled)
                {
                    currentDashQCooldown = DashQCooldown;
                    _anim.SetTrigger(onDash);
                    updateCoolDownAllSkills();
                }

                if (_isAiming)
                {
                    _anim.SetLayerWeight(2, 1); 
                }
                else
                {
                    _anim.SetLayerWeight(2, 0);
                }
                if (Input.GetKeyDown(KeyCode.E) && mana >= 10 && currentBlockECooldown==0 && _isGrounded)
                {
                    
                    ;
                    if (_isAiming)
                    {

                        mouseSpawn.CreateWall();

                    }
                    else
                    {
                        mouseSpawn.CreateBlockingWall();
                        _anim.SetTrigger(onBlock);

                    }
                    loseMana(10);
                    currentBlockECooldown = BlockECooldown;
                    updateCoolDownAllSkills();

                }
                if (Input.GetKeyDown(KeyCode.R) && _isAiming && mana >= 10 && currentAttackRCooldown == 0 && _isGrounded && abilityRIsEnabled)
                {
                    _anim.SetTrigger(onCallBird);

                    if (_isAiming)
                    {
                        _anim.SetTrigger(onCallBird);
                        mouseSpawn.CallBird();

                    }
                    loseMana(10);
                    currentAttackRCooldown = AttackRCooldown;
                    updateCoolDownAllSkills();

                }
                Die();
            }
        }
    }

    private void FixedUpdate()
    {

        if ((base._xAxis != 0 || base._zAxis != 0) && !_isAttacking && !isCreatingPlatform)
        {
            _isMoving = true;
            movementAnimationControl(_speedMovementAnimation);
        }
        else
        {
            base._anim.SetFloat(base._zAxisName, base._zAxis);
            base._anim.SetFloat(base._xAxisName, base._xAxis);
            _isMoving = false;
        }

    }

    private void updateCoolDownAllSkills()
    {
        if (!isRefreshingSkills)
        {
            if (currentBlockECooldown == BlockECooldown || currentAttackRCooldown == AttackRCooldown || currentDashQCooldown == DashQCooldown)
            {
                isRefreshingSkills = true;
                StartCoroutine(UpdateCooldowns());
            }
        }
        

    }
    IEnumerator UpdateCooldowns()
    {
        if (currentAttackRCooldown > 0)
        {
            currentAttackRCooldown = currentAttackRCooldown - 0.1 > 0 ? currentAttackRCooldown -= 0.1f : 0;
        }
        if (currentBlockECooldown > 0)
        {
            currentBlockECooldown = currentBlockECooldown - 0.1 > 0 ? currentBlockECooldown -= 0.1f : 0;
        }
         if(currentDashQCooldown > 0)
        {
            currentDashQCooldown = currentDashQCooldown - 0.1 > 0? currentDashQCooldown-=  0.1f : 0;
        }

        if(currentDashQCooldown == 0 && currentBlockECooldown ==0 && currentAttackRCooldown==0)
        {
            isRefreshingSkills = false;
            yield break;
        }
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(UpdateCooldowns());


    }

    protected void movementAnimationControl(int _speedMovementAnimation)
    {
        if (_isAiming && _isGrounded)
        {

            _speedMovementAnimation = 4;
            SetRigidBodySpeed(PlayerSpeedConstants.stealth);

        }
        else if (_isRunning && _isGrounded)
        {
            _speedMovementAnimation = 1;
            SetRigidBodySpeed(PlayerSpeedConstants.run);
        }
        else
        {
            _speedMovementAnimation = 2;
            SetRigidBodySpeed(PlayerSpeedConstants.walk);
        }
        base._anim.SetFloat(base._xAxisName, base._xAxis /
            _speedMovementAnimation, 0.05f, Time.deltaTime);
        base._anim.SetFloat(base._zAxisName, base._zAxis /
            _speedMovementAnimation, 0.05f, Time.deltaTime);

        base.Movement(_speedMovementAnimation);
    }
    public bool isMoving()
    {
        return _isMoving;
    }

    public bool getIsAiming()
    {
        return _isAiming;
    }

    public IEnumerator jump()
    {
        if (!_isGrounded)
        {
            yield break;
        }
        _anim.SetTrigger("onJump");
        yield return new WaitForSeconds(0.6f);
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CollisionFlags.Below != 0 && !_isGrounded)
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    _isGrounded = true;
                    _canJump = true;
                    _anim.SetBool("isGrounded", true);
                    if(startOfFall - transform.position.y > 8)
                    {
                        base.ReceiveDamage(20);
                    }
                    Debug.Log("El jugador cay? " + (startOfFall - transform.position.y) + " Unidades"); //if( startOfFall)
                }
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (CollisionFlags.Below != 0)
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    _isGrounded = true;
                    _anim.SetBool("isGrounded", true);
                }
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CollisionFlags.Below != 0)
        {
            StartCoroutine(delayfall());
        }
    }

    IEnumerator delayfall()
    {
        yield return new WaitForSeconds(0.5f);
        _isGrounded = false;
        startOfFall = transform.position.y;
        _anim.SetBool("isGrounded", false);
    }
    public int getMana() { return this.mana; }


    IEnumerator CreationIsOnCooldown()
    {
        creationCooldown = true;

        yield return new WaitForSecondsRealtime(BlockECooldown);
        creationCooldown = false;
    }
    public void receiveDamage(int dmg)
    {
        if (life >= 20)
        {
            life -= 20;
        }
        else
        {
            life = 0;
        }
    }

    public void cure(int cureLife)
    {

        base.life = cureLife;
        /*if (base.life + cureLife >= 80)
        {
            base.life = 100;
        }
        else
        {
            base.life += cureLife;
        }
        */
    }

    public void loseMana(int manaLoose)
    {
        if (mana >= manaLoose)
        {
            mana -= manaLoose;
        }
        else
        {
            mana = 0;
        }
    }


    public void receiveMana(int receiveMana)
    {
            mana = receiveMana;
    }

    public Rigidbody GetRigidBody()
    {
        return _rb;
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }
    public void setIsAttacking(bool isAttacking)
    {
        this._isAttacking = isAttacking;
    }

    public void setOnCreateWall()
    {
        _anim.SetTrigger(onCreateWall);
    }

    public void setOnCreateGround()
    {
        _anim.SetTrigger(onCreateGround);
    }
    public void setOnBlock()
    {
        _anim.SetTrigger(onBlock);
    }
    public void setIsCreatingPlatform(bool valor)
    {
        isCreatingPlatform = valor;
    }

    public bool getIsCreatingPlatform()
    {
        return isCreatingPlatform;
    }

    public bool GetCreationCooldown()
    {
        return creationCooldown;
    }
    public void SetisAiming(bool isAiming)
    {
        _isAiming = isAiming;
    }
    public bool getIsDashing()
    {
        return _isDashing;
    }
    public void setIsDashing(bool isDashing)
    {
        _isDashing = isDashing;
    }

    public MouseSpawnWallController getMouseSpawnWallController()
    {
        return mouseSpawn;
    }

    IEnumerator ComboSystem()
    {
        if (comboTimer <= 0)
        {
            _isComboSystemActive = false;
            _comboAttack = 0;
            yield break;
        }
        yield return new WaitForSecondsRealtime(1f);
        comboTimer -= 1;
        StartCoroutine(ComboSystem());
    }

    public void setCheckPoint(Vector3 checkpoint)
    {
        this.checkpoint = checkpoint;
    }
    public virtual void ForceDie()
    {
        life = 0;
    }

    public virtual void Die()
    {
        if (life <= 0)
        {

            IsDead = true;
            _anim.SetTrigger(onDeath);
             _isAttacking =false;
            _isDashing = false;
            _isJumping = false;
            //Debug.Log("destruyo: " + this.name);
            Cursor.lockState = CursorLockMode.Confined;
            eventManager.PlayerDie();
        }
    }
    public void Revive()
    {
        EventManager.instance.PlayerRevive();
        this.transform.position = checkpoint;
        life = 100;
        mana = 100;
        IsDead = false;
        _anim.Play("Walk 0");
    }

    public bool getIsDead()
    {
        return IsDead;
    }

    public void enableAbility(string ability)
    {
        if(ability == "R")
        {
            abilityRIsEnabled = true;
            EventManager.instance.AbilityEnabled("R");
        }
        if(ability == "Q")
        {
            abilityQIsEnabled = true;
            EventManager.instance.AbilityEnabled("Q");
        }
    }
    public float getBlockECooldown() => BlockECooldown;
    public float getAttackRCooldown() => AttackRCooldown;
    public float getDashQCooldown() => DashQCooldown;
    public float getCurrentBlockECooldown() => currentBlockECooldown;
    public float getCurrentAttackRCooldown() => currentAttackRCooldown;
    public float getCurrentDashQCooldown() => currentDashQCooldown;
    

}