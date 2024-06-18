using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject soundPanel;
    public static bool _isPause;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isPause == false)
            {
                Cursor.lockState = CursorLockMode.None;   // keep confined to center of screen
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
        controlPanel.SetActive(false);
        soundPanel.SetActive(false);
        _isPause = false;

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;   // keep confined to center of screen
    }

    public void BackToMenu(string nameMenu)
    {
        Resume();
        SceneManager.LoadScene(nameMenu);
    }

    public void GoToControls()
    {
        pauseMenuPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void GoToSounds()
    {
        pauseMenuPanel.SetActive(false);
        soundPanel.SetActive(true);
    }

    public void ControlsBack()
    {
        pauseMenuPanel.SetActive(true);
        soundPanel.SetActive(false);
        controlPanel.SetActive(false);
    }

    public void ToggleAllSounds()
    {
        SoundManagerController.instance.ToggleAllSounds();
    }

    public void ToggleMusic()
    {
        SoundManagerController.instance.ToggleMusic();
    }

    public void ToggleFX()
    {
        SoundManagerController.instance.ToggleFx();
    }

    public void ToggleAmbient()
    {
        SoundManagerController.instance.ToggleAmbient();
    }
    public void EnableButton(GameObject button)
    {
        button.SetActive(true);
    }
    public void DisableButton(GameObject button)
    {
        button.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

}
