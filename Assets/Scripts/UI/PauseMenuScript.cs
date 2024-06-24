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
    [SerializeField] GameObject diePanel;
    public static bool _isPause;
    PlayerOneScript playerOne;
    public AudioSource audioSource;
    [SerializeField] protected AudioClip audioClip;

    void Start()
    {
        playerOne = GameManager.instance.Player.GetComponent<PlayerOneScript>();
        EventManager.OnDie += ToggleDead;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (_isPause == false)
        {
            Cursor.lockState = CursorLockMode.None;   // keep confined to center of screen
            if (!playerOne.IsDead)
            {
                pauseMenuPanel.SetActive(true);
            }
            else
            {
                diePanel.SetActive(true);
            }
            _isPause = true;

            Time.timeScale = 0;
        }
        else if (_isPause == true)
        {
            Resume();
        }
    }

    public void ToggleDead()
    {
            Cursor.lockState = CursorLockMode.None;   
            diePanel.SetActive(true);
            _isPause = true;
            StartCoroutine(timeScaleChange());
    }

    IEnumerator timeScaleChange()
    {
        yield return new WaitForSeconds(4);
        if(playerOne.IsDead)
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        controlPanel.SetActive(false);
        soundPanel.SetActive(false);
        diePanel.SetActive(false);
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
        diePanel.SetActive(false);
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

    public void LastCheckPoint()
    {
        Resume();
        playerOne.Revive();
    }

    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ClickSound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
