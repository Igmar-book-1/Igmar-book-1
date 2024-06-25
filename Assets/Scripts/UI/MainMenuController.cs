using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Linq;

//TP2 - Florencia Pak
public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject instructionsPanel;
    [SerializeField] GameObject creditsPanel;

    DontDestroyOnLoad dontDestroyOnLoad;
    private AudioSource _audioSource;

    private void Awake()
    {
        dontDestroyOnLoad = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DontDestroyOnLoad>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        dontDestroyOnLoad.addToListScenesToLoad(SceneManager.LoadSceneAsync("Intro"));
        dontDestroyOnLoad.deactivateSceneToLoad(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string sceneName)
    {
        EventManager.instance.EndScene(0);

        dontDestroyOnLoad.addToListScenesToLoad(SceneManager.LoadSceneAsync("Igmar-World 1", LoadSceneMode.Additive));
        dontDestroyOnLoad.deactivateSceneToLoad(1);
        dontDestroyOnLoad.EndScene(0);



        // TODO: Unload all data from previous sessions, then start clean

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

    public void LevelPrototype(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
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
