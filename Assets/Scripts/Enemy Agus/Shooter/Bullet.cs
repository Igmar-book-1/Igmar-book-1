using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject Explosion;
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    [SerializeField] int damage;

    
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * speed);
        Destroy(gameObject, lifetime);
    }

        
   
    
    void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.layer != 7)
        {
            GameObject go = Instantiate(Explosion, transform.position, transform.rotation);
            go.SetActive(true);
            

            if (col.tag == ("Player"))
            {
                col.GetComponent<PlayerOneScript>().receiveDamage(damage);
            }

            Destroy(go, 1);
            Destroy(this.gameObject);
        }
                      
    }
}
