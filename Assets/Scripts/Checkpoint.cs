using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public Light fireLight; 
    private bool activated = false;

    public PlayerCheckpoint playerCheckpoint;
    [SerializeField] GameObject player;

    private void Awake()
    {
        fireParticles.Stop();
        fireLight.enabled = false;
    }

    private void Start()
    {
        playerCheckpoint = player.GetComponent<PlayerCheckpoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & activated == false)
        {
            ActivateCheckpoint();
        }
    }
    public void ActivateCheckpoint()
    {
        activated = true;

        if (fireParticles != null)
        {
            fireParticles.Play();
            fireLight.enabled = true;
        }

        playerCheckpoint.SaveCheckpoint(player.transform.position);
        Debug.Log("Checkpoint actualizado");
    }

    // Método para verificar si el checkpoint está activado
    public bool IsActivated()
    {
        return activated;
    }
}
