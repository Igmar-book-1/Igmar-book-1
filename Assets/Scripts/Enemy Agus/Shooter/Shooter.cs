using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : AllCharacterController
{
    bool detected;
    GameObject target;
    public Transform enemy;
    public GameObject bullet;
    public Transform shootPoint;
 
    public float fireRate;
    float originalTime;
    void Start()
    {
        originalTime = fireRate;
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detected && !IsDead)
        {
            enemy.LookAt(target.transform);
        }
        base.Die();
    }

    private void FixedUpdate()
    {
        if (detected && !IsDead)
        {
            fireRate -= Time.deltaTime;

            if(fireRate < 0)
            {
                ShootPlayer();
                fireRate = originalTime;
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        else
        {
            detected = true;
            target = other.gameObject;
            _anim.SetBool("IsShooting", true);

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            detected = false;
            target = null;
            _anim.SetBool("IsShooting", false);
        }
    }



    private void ShootPlayer()
    {
        GameObject go = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        go.SetActive(true);
        
    }
}
