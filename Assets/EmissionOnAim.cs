using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionOnAim : MonoBehaviour
{
    PlayerOneScript playerOneScript;
    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        playerOneScript = GameManager.instance.Player.GetComponent<PlayerOneScript>();
        particle = this.gameObject.GetComponent<ParticleSystem>();
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerOneScript != null && playerOneScript.getIsAiming())
        {
            if(particle.isEmitting==false)
            {
                particle.Play();
                Debug.Log("Se activo emision");
            }
            
        }
        else
        {
            if (particle.isEmitting == true)
            {
                particle.Stop();
                Debug.Log("Se desactivo emision");
            }
        }
    }
}
