using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneRigidBody  : CharacterController
{
    protected bool _isJumping = false;
    protected bool _onAttack = false;
    protected int _comboAttack= 0;
    protected bool _isAttacking = false;
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

        base._anim.SetFloat(base._xAxisName, base._xAxis);
        base._anim.SetFloat(base._zAxisName, base._zAxis);

        if (_isJumping)
        {
            base._anim.SetTrigger(onJump);
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

        if (base._xAxis != 0 || base._zAxis != 0)
        {
            base.movement();
        }
    }

    private void FixedUpdate()
    {
       
    }

    protected override void movement()
    {
        base.movement();

        
    }

}
