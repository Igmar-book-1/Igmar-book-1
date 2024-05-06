using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuPanel;
    public static bool _isPause;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(_isPause == false)
            {
                pauseMenuPanel.SetActive(true);
                _isPause = true;

                Time.timeScale = 0;
            }
            else if(_isPause == true)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        _isPause = false;

        Time.timeScale = 1;
    }

    public void BackToMenu(string nameMenu)
    {
        Resume();
        SceneManager.LoadScene(nameMenu);
    }

    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

}
