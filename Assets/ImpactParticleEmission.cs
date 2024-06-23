using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticleEmission : MonoBehaviour
{
    private ParticleSystem attackParticles;
    private ParticleSystem[] impactParticles;

    // Start is called before the first frame update
    void Start()
    {
        this.attackParticles = GetComponent<ParticleSystem>();
        this.impactParticles = GetComponentsInChildren<ParticleSystem>();
        Destroy(gameObject, 2f);
    }

    private void OnEnable()
    {
        attackParticles.Play();
        foreach (ParticleSystem particles in impactParticles)
        {
            particles.Play();
        }
        

    }

    private void OnDisable()
    {
        attackParticles.Stop();
        foreach (ParticleSystem particles in impactParticles)
        {
            particles.Stop();
        }

    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
