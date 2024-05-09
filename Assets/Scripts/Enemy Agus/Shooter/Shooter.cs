using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        if (detected)
        {
            enemy.LookAt(target.transform);
        }
    }

    private void FixedUpdate()
    {
        if (detected)
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
        if (other.tag == "Player")
        {
            detected = true;
            target = other.gameObject;
        }
    }

    private void ShootPlayer()
    {
        GameObject go = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        go.SetActive(true);
        
    }
}
