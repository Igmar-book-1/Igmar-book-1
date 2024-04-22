using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneRigidBody  : CharacterController
{
    protected bool _isJumping = false;
    protected bool _onAttack = false;
    protected int _comboAttack= 0;
    protected bool _isAttacking = false;
    protected bool _isAiming = false;
    protected bool _isRunning = false;
    protected bool _isGrounded = false;
    [SerializeField] protected float jumpForce = 0;
    [SerializeField] protected bool _isMoving = false;
    [SerializeField] protected string onJump = "onJump";
    [SerializeField] protected string onAttack = "onAttack";
    [SerializeField] protected string isAttacking = "isAttacking";
    [SerializeField] protected string comboAttack= "comboAttack";

    // Update is called once per frame
    void Update()
    {
        base._xAxis = Input.GetAxis("Horizontal");
        base._zAxis = Input.GetAxis("Vertical");
        _isJumping = Input.GetButtonDown("Jump");
        _onAttack = Input.GetButtonDown("Fire1");
        _isAiming = Input.GetButton("Fire2");
        _anim.SetFloat("verticalSpeed", _rb.velocity.y);

        base._anim.SetFloat(base._xAxisName, base._xAxis);
        base._anim.SetFloat(base._zAxisName, base._zAxis);

        if (_isJumping)
        {
            StartCoroutine(jump());
           
        }

        if (_onAttack)
        {
            base._anim.SetTrigger(onAttack);
            base._anim.SetInteger(comboAttack, _comboAttack);
            if (_comboAttack < 2)
            {
                _comboAttack++;
            }
            else
            {
                _comboAttack = 0;

            }

        }

        if (base._xAxis != 0 || base._zAxis != 0 )
        {
            _isMoving = true;
            base.movement();
        }
        else
        {
            _isMoving = false;
        }

    }

    private void FixedUpdate()
    {
       
    }

    protected override void movement()
    {

        base.movement();
    }
    public bool isMoving()
    {
        return _isMoving;
    }

    public bool isAiming()
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
        if(collision.gameObject.layer == LayerConstants.floor)
        {
            _isGrounded = true;
            _anim.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerConstants.floor)
        {
            _isGrounded = false;
            _anim.SetBool("isGrounded",false);
        }
    }

}
