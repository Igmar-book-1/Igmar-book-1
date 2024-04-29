using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerOneMovement 
{
    protected bool _isGrounded = true;
    protected Animator _anim;
    protected Rigidbody _rb;
    protected float jumpForce;

    public PlayerOneMovement( Animator _anim, Rigidbody _rb, float jumpForce)
    {
        this._anim = _anim;
        this._rb = _rb;
        this.jumpForce = jumpForce;
    }


    public IEnumerator Jump(bool _isGrounded)
    {
        if (!_isGrounded)
        {
            yield break;
        }
        _anim.SetTrigger("onJump");
        yield return new WaitForSeconds(0.2f);
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public bool isGroundedEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerConstants.floor)
        {
            _isGrounded = true;
            _anim.SetBool("isGrounded", true);
        }
        return _isGrounded;
    }

    public bool isGroundedExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerConstants.floor)
        {
            _isGrounded = false;
            _anim.SetBool("isGrounded", false);
        }
        return _isGrounded;
    }

}
