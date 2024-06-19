using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void dieAction();
    public static event dieAction OnDie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDie()
    {
        if ( OnDie != null)
        {
            OnDie();
        }
    }
}
