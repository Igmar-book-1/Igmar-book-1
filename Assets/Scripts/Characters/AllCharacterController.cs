using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public abstract class AllCharacterController : MonoBehaviour
{

    [Header("Parameters")]
    protected Rigidbody _rb;
    [SerializeField] float rigidBodySpeed = 0;
    protected float _xAxis = 0;
    protected float _yAxis = 0;
    protected float _zAxis = 0;
    private bool _isDead = false;
    [SerializeField] protected int life = 100;
    DamageController damageController;
    protected bool isHurt = false;
    [SerializeField] float backOnHurt = 50;

    protected Animator _anim;

    [Header("Animation")]
    [SerializeField] protected string _xAxisName = "xAxis";
    [SerializeField] protected string _zAxisName = "zAxis";
    protected string onDeath = "onDeath";
    protected string onHurt = "onHurt";

    public bool IsDead { get => _isDead; set => _isDead = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _anim = GetComponentInChildren<Animator>();
        damageController = new DamageController(this);
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


        Debug.Log(this.name + " Receives Damage");
        life -= damage;
        if (!(this.gameObject.tag == "Player" && this.gameObject.GetComponentInChildren<PlayerOneScript>().IsAttacking()))
        {
            _anim.SetTrigger(onHurt);

        }
        isHurt = true;
        if (!isHurt && this.tag == "Enemy")
        {
            _rb.AddForce(Vector3.back * backOnHurt, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {

            if (collision.rigidbody.tag == "Player")
            {
                damageController.OnHitEnemy(collision);
            }
        }
    }

    public DamageController GetDamageController()
    {
        return damageController;
    }

    public virtual void Die()
    {
        if (life <= 0)
        {

            IsDead = true;
            _anim.SetTrigger(onDeath);
            Debug.Log("destruyo: " + this.name);
            Destroy(this.gameObject, 10f);
            if (this.tag == "Player")
            {
                StartCoroutine(reloadScene());
            }

        }
    }

    public virtual void ForceDie()
    {
        IsDead = true;
        _anim.SetTrigger(onDeath);
        Debug.Log("destruyo: " + this.name);
        Destroy(this.gameObject, 10f);
        if (this.tag == "Player")
        {
            StartCoroutine(reloadScene());
        }
    }


    IEnumerator reloadScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(1);
    }

    public void setIsHurtFalse()
    {
        isHurt = false;
    }

}
