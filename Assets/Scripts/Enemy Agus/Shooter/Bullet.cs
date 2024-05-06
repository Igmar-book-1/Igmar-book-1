using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    [SerializeField] int damage;

    public bool ownedByPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            other.GetComponent<PlayerOneScript>().receiveDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
