using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

//TP2 - Matias Sueldo
public class DontDestroyOnLoad : MonoBehaviour
{
    private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

    private List<AsyncOperation> _scenesToUnload = new List<AsyncOperation>();

    [SerializeField] GameObject canvasFade;

    private Animator anim;
    

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        anim = canvasFade.GetComponent<Animator>();

        EventManager.OnEndScene += EndScene;
    }

    public void addToListScenesToLoad(AsyncOperation sceneToLoad)
    {
        _scenesToLoad.Add(sceneToLoad);
    }
    public AsyncOperation getToListScenesToLoad(int sceneToLoad)
    {
        return _scenesToLoad[sceneToLoad];
    }

    public void removeSceneToLoad(int sceneToLoad)
    {
        _scenesToLoad.RemoveAt(sceneToLoad);
    }

    public void addToListScenesToUnload(AsyncOperation sceneToLoad)
    {
        _scenesToUnload.Add(sceneToLoad);
    }
    public AsyncOperation getToListScenesToUnload(int sceneToLoad)
    {
        return _scenesToUnload[sceneToLoad];
    }

    public void removeSceneToUnload(int sceneToLoad)
    {
        _scenesToUnload.RemoveAt(sceneToLoad);
    }

    public void deactivateSceneToLoad(int scene)
    {
        _scenesToLoad[scene].allowSceneActivation = false;
    }
    public void activateSceneToLoad(int scene)
    {
        _scenesToLoad[scene].allowSceneActivation = true;
        canvasFade.SetActive(true);
        anim.Play("Start");
        StartCoroutine(StopTransition());
    }

    public void EndScene(int scene)
    {
        canvasFade.SetActive(true);
        anim.enabled = true;
        anim.Play("End");
        StartCoroutine(Esperar(scene));

    }

    IEnumerator Esperar(int scene)
    {
        yield return new WaitForSeconds(2f);
        activateSceneToLoad(scene);
    }

    IEnumerator StopTransition()
    {
        yield return new WaitForSeconds(2f);
        anim.enabled = false;
        canvasFade.SetActive(false);
    }
}
