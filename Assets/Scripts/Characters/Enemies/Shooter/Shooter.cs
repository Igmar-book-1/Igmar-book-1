using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//TP2 - Agustin Picchio
public class Shooter : AllCharacterController
{
    bool detected;
    GameObject target;
    public Transform enemy;
    public GameObject bullet;
    public Transform shootPoint;
    public Transform lookAtTarget;
 
 
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
            //lookAtTarget = target.transform;
            // Vector3 lookAtPoint = target.transform.position; 
             // lookAtPoint.y = 180f;
             enemy.LookAt(target.transform); 
            //Quaternion newRotation = Quaternion.LookRotation(transform.position - target.transform.position, Vector3.forward);
           /* newRotation.z = 0f;
            newRotation.x = 0f;
            newRotation.y =
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 8);*/
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
        AttackSound();
        GameObject go = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        go.SetActive(true);
        
    }
}
