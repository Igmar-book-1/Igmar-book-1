using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public Light fireLight; 
    private bool activated = false;

    public PlayerCheckpoint playerCheckpoint;
    private GameObject player;
    private AudioSource[] sounds;

    private void Awake()
    {
        fireParticles.Stop();
        fireLight.enabled = false;
    }

    private void Start()
    {
        player = GameManager.instance.Player;
        playerCheckpoint = player.GetComponent<PlayerCheckpoint>();
        sounds = GetComponentsInChildren<AudioSource>();

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
            StartCoroutine(soundCorroutine());
            fireParticles.Play();
            fireLight.enabled = true;
        }

        player.GetComponent<PlayerOneScript>().setCheckPoint(player.transform.position);
        //Debug.Log("Checkpoint actualizado");
    }

    // Método para verificar si el checkpoint está activado
    public bool IsActivated()
    {
        return activated;
    }
    
    IEnumerator soundCorroutine()
    {
        sounds[0].Play();
        yield return new WaitForSeconds(sounds[0].clip.length);
        sounds[1].Play();
    }
}
