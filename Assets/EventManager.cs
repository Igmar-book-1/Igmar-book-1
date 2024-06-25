using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Matias Sueldo
public class EventManager : MonoBehaviour
{
    public static EventManager instance { get; private set; }
    public delegate void dieAction();
    public static event dieAction OnDie;

    public delegate void HurtAction();
    public static event HurtAction OnHurt;

    public delegate void ReviveAction();
    public static event ReviveAction OnRevive;

    public delegate void EndSceneAction(int scene);
    public static event EndSceneAction OnEndScene;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerDie()
    {
        if (OnDie != null)
        {
            OnDie();
        }
    }

    public void PlayerHurt()
    {
        if (OnHurt != null)
        {
            OnHurt();
        }
    }
    public void PlayerRevive()
    {
        if (OnRevive != null)
        {
            OnRevive();
        }
    }

    public void EndScene(int scene)
    {
        OnEndScene(scene);
    }
}
