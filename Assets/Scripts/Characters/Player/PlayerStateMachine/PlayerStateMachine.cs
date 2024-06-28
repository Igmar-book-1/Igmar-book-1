using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    static PlayerState CurrentState;
    // Start is called before the first frame update
    /*

    // Update is called once per frame
    static void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }

    static void setNextState(PlayerState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }*/
}
