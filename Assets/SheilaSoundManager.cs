using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheilaSoundManager : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSourceMouth;
    [SerializeField] protected AudioSource audioSourceFeet;
    [SerializeField] protected AudioClip sheilaHurtWeak;
    [SerializeField] protected AudioClip sheilaHurtHard;
    [SerializeField] protected AudioClip sheilaTalk;
    [SerializeField] protected AudioClip sheilaAttack;
    [SerializeField] protected AudioClip sheilaDead;
    [SerializeField] protected AudioClip sheilaJump;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackSound()
    {
        audioSourceMouth.clip =sheilaAttack;
        audioSourceMouth.Play();
    }

    public void HurtWeakSound()
    {
        audioSourceMouth.clip = sheilaHurtWeak;
        audioSourceMouth.Play();
    }
    public void HurtHardSound()
    {
        audioSourceMouth.clip = sheilaHurtHard;
        audioSourceMouth.Play();
    }

    public void JumpSound()
    {
        audioSourceMouth.clip = sheilaJump;
        audioSourceMouth.Play();
    }
    public void DeadSound()
    {
        audioSourceMouth.clip = sheilaDead;
        audioSourceMouth.Play();
    }
    public void TalkSound()
    {
        audioSourceMouth.clip = sheilaTalk;
        audioSourceMouth.Play();
    }
}
