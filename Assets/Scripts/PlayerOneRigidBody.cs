using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneRigidBody : MonoBehaviour
{
    [Header("Parameters")]
    private Rigidbody _rb;
    [SerializeField] float rigidBodySpeed = 0;
    private float _xAxis=0;
    private float _yAxis = 0;
    private float _zAxis = 0;
    private Animator _anim;

    [Header("Animation")]
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        _anim.SetFloat(_xAxisName, _xAxis);
        _anim.SetFloat(_zAxisName, _zAxis);

        if (_xAxis !=0 || _zAxis != 0)
        {
            Movement();
        }
    }

    private void Movement()

    {
        Vector3 dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;
        _rb.MovePosition(transform.position += dir * rigidBodySpeed * Time.fixedDeltaTime);
    }
}
