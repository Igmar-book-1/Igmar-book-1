using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSoundController 
{
    private List<AudioSource> hurt;
    private List<AudioSource> attacks;
    private List<AudioSource> dead;
    private object value2;
    private object value3;
    private System.Func<bool, AudioSource[]> value;

    public CharacterSoundController(List<AudioSource> hurt,List<AudioSource> attacks, List<AudioSource> dead)
    {
        this.dead = dead;
        this.attacks = attacks;
        this.hurt = hurt;
    }

    public void onHurtPlay()
    {
        int random = Random.Range(0, hurt.Count-1);
        hurt.GetRange(random, 1).FirstOrDefault().Play();
    }

    public void onDeadPlay()
    {
        int random = hurt.Count-1;
        foreach(AudioSource source in hurt)
        {
            source.Stop();
        }
        hurt.GetRange(random, 1).FirstOrDefault().Play();
    }

    public void onAttackPlay()
    {
        int random = hurt.Count - 1;
        foreach (AudioSource source in hurt)
        {
            source.Stop();
        }
        hurt.GetRange(random, 1).FirstOrDefault().Play();
    }
}
