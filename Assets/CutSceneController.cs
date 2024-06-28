using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    [SerializeField] protected GameObject MainCamera;
    [SerializeField] protected GameObject CutsceneCamera;
    [SerializeField] protected GameObject UICanva;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwichCamera(){
        MainCamera.SetActive(true);
        CutsceneCamera.SetActive(false);
        UICanva.SetActive(true);
    }

}
