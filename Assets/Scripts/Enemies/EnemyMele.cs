using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMele : EnemyController
{

    [SerializeField] Transform _basePosition;
    private Transform _targetPlayer;

    [SerializeField] float _distChase;
    [SerializeField] float _minDist;
    [SerializeField] float _maxDist;
    [SerializeField] float _speed;

    void Start()
    {
        _targetPlayer = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        base.Die();
    }

    void FixedUpdate()
    { 
        if (_targetPlayer != null && Vector3.Distance(transform.position, _targetPlayer.position) < _distChase)
        {    
            Vector3 _dir = _targetPlayer.position - transform.position;
            transform.forward = _dir;
                
            if (Vector3.Distance(transform.position, _targetPlayer.position) > _minDist)
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
                //Debug.Log("Persigo al player");
                if (Vector3.Distance(transform.position, _targetPlayer.position) <= _minDist)
                {
                    Attack();
                }
            }
        }
        else
        {
            Vector3 _dir = _basePosition.position - transform.position;
            transform.forward = _dir;
                
            if (Vector3.Distance(transform.position, _basePosition.position) > _minDist)
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
                //Debug.Log("Vuelvo a la base");
            }
        }
    }

    void Attack()
    {
        Debug.Log("Ataque");
    }
    public override void Die()
    {
        if (life <= 0)
        {
            Debug.Log("destruyo: " + this.name);
            Destroy(this.gameObject.transform.parent.gameObject);

        }
    }
}
