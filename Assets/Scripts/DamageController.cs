using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController
{
    AllCharacterController allCharacterController;
    // Start is called before the first frame update
    public DamageController(AllCharacterController allCharacterController)
    {
        this.allCharacterController = allCharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHitEnemy(Collision collision) 
    {

            Hurt(collision.rigidbody.gameObject.GetComponent<AllCharacterController>());
    }

    public void OnHitEnemy(Collider collision)
    {
        if(collision.gameObject.GetComponentInParent<AllCharacterController>() != null)
        {
            Hurt(collision.gameObject.GetComponentInParent<AllCharacterController>());

        }
    }

    void Hurt(AllCharacterController rigidbody)
    {
        rigidbody.ReceiveDamage(20);
    }
}
