using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TP2 - Matias Sueldo
public class DoorScript : MonoBehaviour
{
    DontDestroyOnLoad dontDestroyOnLoad;
    
    void Start()
    {
        dontDestroyOnLoad = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DontDestroyOnLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            SceneManager.LoadScene("Continuara");
        }
    }
}
