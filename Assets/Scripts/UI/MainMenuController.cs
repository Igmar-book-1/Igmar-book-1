using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject instructionsPanel;
    [SerializeField] GameObject creditsPanel;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string sceneName)
    {
        
        // TODO: Unload all data from previous sessions, then start clean
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Instructions()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void GoToGoal()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(false);
    }

    public void Credits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainPanel.SetActive(true);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    private void PlaySound(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
