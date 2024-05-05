using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMele : EnemyController
{
    [Header("ParametersMele")]
    [SerializeField] Transform _basePosition;
    private Transform _targetPlayer;
    private BoxCollider _box;

    private NavMeshAgent agent;

    [SerializeField] float _distChase;
    [SerializeField] float _minDistPlayer;
    [SerializeField] float _minDistBase;
    [SerializeField] float _maxDist;
    [SerializeField] float _speed;

    private bool _isDead = false;
    private bool isAttackAllowed = true;

    //Parametros de animator
    private bool _isRunning = false;
    protected string onAttack = "onAttack";
    protected string isRunning = "isRunning";
    protected string onDeath = "onDeath";
    protected string onHurt = "onHurt";
    protected string onBase = "onBase";


    void Start()
    {
        _box = GetComponent<BoxCollider>();
        _targetPlayer = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    protected void movementAnimationControl(int _speedMovementAnimation)
    {

        if (_isRunning)
        {
            _anim.SetBool(isRunning, _isRunning);
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

    void FixedUpdate()
    {
        //Perseguir al jugador
        if (_targetPlayer != null && _isDead == false && Vector3.Distance(transform.position, _targetPlayer.position) < _distChase)
        {
            _isRunning = true;
            _anim.SetBool(isRunning, _isRunning);

            agent.SetDestination(_targetPlayer.transform.position);
            agent.speed = 3.5f;

            transform.LookAt(_targetPlayer);
            float distancia = Vector3.Distance(transform.position, _targetPlayer.position);

            if (distancia <= _minDistPlayer)
            {
                StartCoroutine(AttackFreeze());
                if (isAttackAllowed)
                {
                    Attack();
                }

            }
        }
        else
        {
            _anim.SetBool(isRunning, false);

            agent.SetDestination(_basePosition.transform.position);

            transform.LookAt(_basePosition);

            if (Vector3.Distance(transform.position, _basePosition.position) > _minDistBase)
            {
                _anim.SetBool(isRunning, _isRunning);
                //Debug.Log("Vuelvo a la base");
            }
            else
            {
                _anim.SetTrigger(onBase);
                agent.speed = 0;
            }
        }
    }

    void Attack()
    {
        _anim.SetTrigger(onAttack);
        Debug.Log("Ataque");
        isAttackAllowed = false;
    }

    IEnumerator AttackFreeze()
    {
        yield return new WaitForSeconds(3f);
        isAttackAllowed = true;
    }
    public override void Die()
    {
        if (life <= 0)
        {
            _isDead = true;

            _anim.SetTrigger(onDeath);
            agent.speed = 0;

            Destroy(_box);
            Debug.Log("destruyo: " + this.name);
            Destroy(this.gameObject.transform.parent.gameObject, 10f);
        }
    }
}
