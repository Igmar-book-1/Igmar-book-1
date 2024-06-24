using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TP2 - Florencia Pak
public class UIDeathMenu : MonoBehaviour
{
    [SerializeField] GameObject DeathPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        DeathPanel.SetActive(false);
    }

    public void BackToMenu(string nameMenu)
    {
        SceneManager.LoadScene(nameMenu);
    }


    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
