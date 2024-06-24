using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CinematicScript : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    public string NextSceneName;

    DontDestroyOnLoad dontDestroyOnLoad;
    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += CheckOver;
    }
    void Start()
    {

        dontDestroyOnLoad = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DontDestroyOnLoad>();

    }

    private void Update()
    {
        /*if(this._asynOperation == null)
        {
            this.StartCoroutine(LoadSceneAsync(NextSceneName));
        }*/

        if (Input.anyKeyDown)
        {
            CheckOver(_videoPlayer);
        }
    }

    public void CheckOver(VideoPlayer vp)
    {
        /*if(this._asynOperation != null)
        {
            this._asynOperation.allowSceneActivation = true;
        }*/
        dontDestroyOnLoad.deactivateSceneToLoad(0);
        dontDestroyOnLoad.activateSceneToLoad(1);
        SceneManager.UnloadScene("Intro");
    }

    /*private IEnumerator LoadSceneAsync(string nameScene)
    {
        this._asynOperation = SceneManager.LoadSceneAsync(nameScene);
        this._asynOperation.allowSceneActivation = false;
        while (!this._asynOperation.isDone)
        {
            Debug.Log("La escena cargó " + this._asynOperation.progress + "%");
            yield return null;
        }
    }*/

}
