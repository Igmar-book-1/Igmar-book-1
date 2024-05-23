using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    private bool isAttackAllowed = true;

    //Parametros de animator
    private bool _isRunning = false;
    protected string onAttack = "onAttack";
    protected string isRunning = "isRunning";
    protected string onBase = "onBase";
    protected string minDistance = "minDistance";


    void Start()
    {
        _box = GetComponent<BoxCollider>();
        _targetPlayer = GameManager.instance.Player.transform;
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
    }

    void FixedUpdate()
    {

        if (!isHurt && !IsDead)
        {

            //Perseguir al jugador
            if (_targetPlayer != null 
                && !_targetPlayer.GetComponent<PlayerOneScript>().IsDead
                && Vector3.Distance(transform.position, _targetPlayer.position) < _distChase)
            {
                _isRunning = true;
                _anim.SetBool(isRunning, _isRunning);

                agent.SetDestination(_targetPlayer.transform.position);

                float distancia = Vector3.Distance(transform.position, _targetPlayer.position);

                if (distancia>=5)
                {

                agent.speed = 4.5f;
                }

                transform.LookAt(_targetPlayer);

                _anim.SetFloat(minDistance, distancia);
                if (distancia <= _minDistPlayer)
                {
                    agent.speed = 0.75f;
                    if (isAttackAllowed)
                    {
                        Attack();
                    }

                }
            }
            else
            {
                _anim.SetFloat(minDistance, 100);
                agent.speed = 4.5f;
                _anim.SetBool(isRunning, false);

                agent.SetDestination(_basePosition.transform.position);

                transform.LookAt(_basePosition);

                 if ((Vector3.Distance(transform.position, _basePosition.position) > _minDistBase) || IsDead)
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
    }

    void Attack()
    {
        isAttackAllowed = false;
        _anim.SetTrigger(onAttack);
        //Debug.Log("Ataque");
        StartCoroutine(AttackFreeze());

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
            IsDead = true;

            _anim.SetTrigger(onDeath);
            agent.speed = 0;

            Destroy(_box);
            //Debug.Log("destruyo: " + this.name);
            Destroy(this.gameObject.transform.parent.gameObject, 10f);
        }
    }
    public void forceBack()
    {
        _rb.AddForce(transform.forward * -1 * 30);
    }
}
