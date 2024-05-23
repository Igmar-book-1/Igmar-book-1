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
    [SerializeField] float backOnHurt = 1;

    protected Animator _anim;

    [Header("Animation")]
    [SerializeField] protected string _xAxisName = "xAxis";
    [SerializeField] protected string _zAxisName = "zAxis";
    protected string onDeath = "onDeath";
    protected string onHurt = "onHurt";

    [SerializeField] float invulnerabilityDuration = 2.0f; // Duración de la invulnerabilidad en segundos


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
        isHurt = _anim.GetCurrentAnimatorStateInfo(0).IsName(onHurt);

    }
    protected virtual void Movement(int speedAnimationController)

    {
        if (!IsDead)
        {


            Vector3 dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;
            _rb.MovePosition(transform.position += dir * rigidBodySpeed * Time.fixedDeltaTime);
        }
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
        if (!isHurt)
        {
            if (this.gameObject.tag == "Player")
            {
                PlayerOneScript playerScript = this.gameObject.GetComponentInChildren<PlayerOneScript>();
                if (playerScript != null && playerScript.IsAttacking())
                {
                    // Recibir daño pero no activar la animación "On Hurt"
                    life -= damage;
                    StartCoroutine(InvulnerabilityCoroutine());
                    return;
                }
            }

            isHurt = true;
            life -= damage;
            _anim.SetTrigger(onHurt);

            // Asegurarse de que el estado isHurt se restablezca al final de la animación "On Hurt"
            //StartCoroutine(ResetIsHurt());
            StartCoroutine(InvulnerabilityCoroutine());
        }

    }
    private IEnumerator ResetIsHurt()
    {
        // Esperar a que termine la animación "On Hurt"
        while (_anim.GetCurrentAnimatorStateInfo(0).IsName(onHurt))
        {
            yield return null;
        }

        // Esperar un cuadro más para asegurarse de que la animación ha terminado
        yield return new WaitForEndOfFrame();

        isHurt = false;
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        // Hacer al personaje invulnerable
        isHurt = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isHurt = false;
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
            //Debug.Log("destruyo: " + this.name);
            Destroy(this.gameObject, 10f);
            if (this.tag == "Player")
            {
                StartCoroutine(reloadScene());
            }

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


    public void BackOnHurt()
    {
        _rb.AddForce(_rb.transform.forward.normalized * (-1) * backOnHurt, ForceMode.Force);
    }
}
