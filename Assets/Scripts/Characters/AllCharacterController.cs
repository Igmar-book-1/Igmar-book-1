using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllCharacterController : MonoBehaviour
{

    [Header("Parameters")]
    protected Rigidbody _rb;
    [SerializeField] float rigidBodySpeed = 0;
    protected float _xAxis = 0;
    protected float _yAxis = 0;
    protected float _zAxis = 0;
    [SerializeField] protected int life = 100;

    protected Animator _anim;

    [Header("Animation")]
    [SerializeField] protected string _xAxisName = "xAxis";
    [SerializeField] protected string _zAxisName = "zAxis";

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void Movement(int speedAnimationController)

    {
        Vector3 dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;
        _rb.MovePosition(transform.position += dir * rigidBodySpeed * Time.fixedDeltaTime);
    }

    protected void SetRigidBodySpeed(float speed)
    {
        rigidBodySpeed = speed;
    }

    protected float GetRigidBodySpeed()
    {
        return rigidBodySpeed;
    }

    public int GetLife() { return life; }

    public void ReceiveDamage(int damage)
    {
        life -= damage;
    }
}
