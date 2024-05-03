using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneScript : AllCharacterController
{
    protected bool _isJumping = false;
    protected bool _onAttack = false;
    protected int _comboAttack = 0;
    protected bool _isAttacking = false;
    protected bool _isAiming = false;
    protected bool _isRunning = false;
    protected bool _isGrounded = false;
    protected int _speedMovementAnimation = 1;
    protected int mana = 100;
    [SerializeField] protected float jumpForce = 0;
    [SerializeField] protected bool _isMoving = false;
    [SerializeField] protected string onJump = "onJump";
    [SerializeField] protected string onAttack = "onAttack";
    [SerializeField] protected string isAiming = "isAiming";
    [SerializeField] protected string isAttacking = "isAttacking";
    [SerializeField] protected string comboAttack = "comboAttack";
    PlayerOneMovement playerOneMovement;
    private MouseSpawnWallController mouseSpawn;
    private StaffDamageScript staff;

    private void Start()
    {
        playerOneMovement = new PlayerOneMovement(this._anim, this._rb, this.jumpForce);
        mouseSpawn = new MouseSpawnWallController();
        mouseSpawn.Start();
        staff = GameObject.FindWithTag("Staff").GetComponent<StaffDamageScript>();
    }
    // Update is called once per frame
    void Update()
    {
        base._xAxis = Input.GetAxis("Horizontal");
        base._zAxis = Input.GetAxis("Vertical");
        _isJumping = Input.GetButtonDown("Jump");
        _onAttack = Input.GetButtonDown("Fire1");
        _isAiming = Input.GetButton("Fire2");
        _isRunning = Input.GetButton("Fire3");
        base._anim.SetBool(isAiming, _isAiming);

        _anim.SetFloat("verticalSpeed", _rb.velocity.y);

        if (_isJumping)
        {
            StartCoroutine(playerOneMovement.Jump(_isGrounded));

        }

        if (_onAttack && _isGrounded)
        {
            base._anim.SetTrigger(onAttack);
            base._anim.SetInteger(comboAttack, _comboAttack);
            if (_comboAttack < 2)
            {
                _comboAttack++;
                if (_comboAttack == 1)
                {
                    StartCoroutine(playerOneMovement.Attack2());

                }
            }
            else
            {
                StartCoroutine(playerOneMovement.Attack3());
                _comboAttack = 0;

            }

        }

        if (base._xAxis != 0 || base._zAxis != 0)
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            receiveDamage();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            cure();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            loseMana();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            receiveMana();
        }

        if(Input.GetKeyDown(KeyCode.E) && _isAiming)
        {
            mouseSpawn.CreateWall();
        }
        base.Die();

    }

    private void FixedUpdate()
    {
        

       

        
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
    private void OnCollisionStay(Collision collision)
    {
        if (CollisionFlags.Below != 0)
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if(Vector3.Dot(contact.normal, Vector3.up)> 0.5)
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
            _isGrounded = false;
            _anim.SetBool("isGrounded", false);
        }
    }

    public int getMana() { return this.mana; }


    public void receiveDamage()
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

    public void cure()
    {
        if (life >= 80)
        {
            life = 100;
        }
        else
        {
            life += 20;
        }
    }

    public void loseMana ()
    {
        if (mana >= 20)
        {
            mana -= 20;
        }
        else
        {
            mana = 0;
        }
    }

    public void receiveMana()
    {
        if (mana >= 80)
        {
            mana = 100;
        }
        else
        {
            mana += 20;
        }
    }

    public Rigidbody GetRigidBody()
    {
        return _rb;
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }
}