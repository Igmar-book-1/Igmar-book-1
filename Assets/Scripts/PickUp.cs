using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerOneScript>() != null)
        {
            OnPickup();
        }
    }

    public abstract void OnPickup();
}
