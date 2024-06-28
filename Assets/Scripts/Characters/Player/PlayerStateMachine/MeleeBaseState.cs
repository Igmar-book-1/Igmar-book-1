using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeBaseState : PlayerState
{
    public float duration;
    protected Animator animator;
    protected bool canCombo;
    protected int attackIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*
    public override void OnEnterState(PlayerStateMachine stateMachine)
    {
        base.OnEnter(stateMachine);
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }*/
}
