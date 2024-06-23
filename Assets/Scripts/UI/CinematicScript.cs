using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CinematicScript : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    public string NextSceneName;
    AsyncOperation _asynOperation;
    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += CheckOver;
    }
    void Start()
    {

    }

    private void Update()
    {
        if(this._asynOperation == null)
        {
            this.StartCoroutine(LoadSceneAsync(NextSceneName));
        }

        if (Input.anyKeyDown)
        {
            CheckOver(_videoPlayer);
        }
    }

    public void CheckOver(VideoPlayer vp)
    {
        if(this._asynOperation != null)
        {
            this._asynOperation.allowSceneActivation = true;
        }
        //SceneManager.LoadScene(NextSceneName, LoadSceneMode.Single);
    }

    private IEnumerator LoadSceneAsync(string nameScene)
    {
        this._asynOperation = SceneManager.LoadSceneAsync(nameScene);
        this._asynOperation.allowSceneActivation = false;
        while (!this._asynOperation.isDone)
        {
            Debug.Log("La escena cargó " + this._asynOperation.progress + "%");
            yield return null;
        }
    }

}
