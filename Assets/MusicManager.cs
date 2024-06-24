using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get; private set; }
    [SerializeField] protected AudioClip[] music;
    [SerializeField] protected AudioSource audioSource;
    private float originalAudioVolume;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalAudioVolume = audioSource.volume;
        playAmbientMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
            playBattleMusic();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            playBattleMusic();
            return;
        }
        playAmbientMusic();

    }
    private void OnTriggerExit(Collider other)
    {
            playAmbientMusic();
    }*/

    public void playAmbientMusic()
    {
        if (!(audioSource.isPlaying && audioSource.clip == music[0]))
        {
            audioSource.clip = music[0];
            audioSource.Play();
        }

    }
    public void playBattleMusic()
    {
        if (!(audioSource.isPlaying && audioSource.clip == music[1]))
        {
            audioSource.clip = music[1];
            audioSource.Play();
        }
        
    }
}
