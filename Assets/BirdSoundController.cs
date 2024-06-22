using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSoundController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] protected AudioClip wingFlaps;
    [SerializeField] protected AudioClip birdAttack;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playWingSound()
    {
        audioSource.clip = wingFlaps;
        audioSource.Play();
    }
    public void playBirdAttackSound()
    {
        audioSource.clip = birdAttack;
        audioSource.volume = 100;
        audioSource.Play();
    }
}
