using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSoundController 
{
    [SerializeField] AudioClip[] hurt;

    [SerializeField] AudioClip[] attacks;
    [SerializeField] AudioClip[] talk;

    [SerializeField] AudioClip[] dead;
    [SerializeField] AudioSource mouth;
    [SerializeField] AudioSource feet;
    [SerializeField] AudioSource weapon;

    private object value2;
    private object value3;
    private System.Func<bool, AudioSource[]> value;

    public CharacterSoundController(AudioClip[] hurt,AudioClip[] attacks, AudioClip[] talk, AudioClip[] dead, AudioSource mouth, AudioSource feet, AudioSource weapon)
    {
        this.dead = dead;
        this.attacks = attacks;
        this.hurt = hurt;
        this.mouth = mouth;
        this.feet = feet;
        this.weapon = weapon;
        this.talk = talk;
    }

    public void onHurtPlay()
    {
            int random = Random.Range(0, hurt.Length);
            mouth.clip = hurt[Random.Range(random, 1)];
            mouth.Play();
    }

    public void onDeadPlay()
    {
        int random = dead.Length-1;
        mouth.Stop();
        mouth.clip = dead[Random.Range(random, 1)];
        mouth.Play();
    }

    public void onAttackPlay()
    {
        if (mouth.isPlaying && !attacks.ToList().Contains(mouth.clip))
        {
            int random = attacks.Length - 1;
            mouth.Stop();
            mouth.clip = attacks[Random.Range(random, 1)];
            mouth.Play();
        }
    }

    public void onTalkPlay()
    {
        if (!(mouth.isPlaying && attacks.ToList().Contains(mouth.clip)))
        {
            int random = Random.Range(0, talk.Length);

            mouth.clip = talk[Random.Range(random, 1)];
            mouth.Play();
        }

    }
    public void onTalkPlay(int number)
    {
        if (!(mouth.isPlaying && attacks.ToList().Contains(mouth.clip)) && !(mouth.isPlaying && talk[number] ==mouth.clip))
        {
            mouth.clip = talk[number];
            mouth.Play();
        }
    }
}
