using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private GameObject player;
    private GameObject moveCamera;
    private GameObject aimCamera;

    public GameObject MoveCamera { get => moveCamera; set => moveCamera = value; }
    public GameObject AimCamera { get => aimCamera; set => aimCamera = value; }
    public GameObject Player { get => player; set => player = value; }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Player = GameObject.FindWithTag("Player");
        aimCamera = GameObject.FindWithTag("MainCamera");

    }
    // Start is called before the first frame update
}
