using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CinematicScript : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    public string NextSceneName;

    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += CheckOver;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            CheckOver(_videoPlayer);
        }
    }

    public void CheckOver(VideoPlayer vp)
    {
        SceneManager.LoadScene(NextSceneName, LoadSceneMode.Single);
    }
}
