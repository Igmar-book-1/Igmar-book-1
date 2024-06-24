using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private GameObject player;
    private GameObject moveCamera;
    private GameObject aimCamera;
    private GameObject interactButton;
    private GameObject mainCamera;
    private GameObject rEnabled;
    private GameObject blockEnabled;
    private GameObject dashEnabled;
    private GameObject platformEnabled;
    private GameObject eventManager;
    private Slider generalSoundSlider;
    private Slider musicSlider;
    private Slider fXSlider;
    private Slider ambientSlider;

    DontDestroyOnLoad dontDestroyOnLoad;


    public Slider GeneralSoundSlider { get => generalSoundSlider; set => generalSoundSlider = value; }
    public Slider MusicSlider { get => musicSlider; set => musicSlider = value; }
    public Slider FxSlider { get => fXSlider; set => fXSlider = value; }
    public Slider AmbientSlider { get => ambientSlider; set => ambientSlider = value; }
    public GameObject MoveCamera { get => moveCamera; set => moveCamera = value; }
    public GameObject EventManager { get => eventManager; set => eventManager = value; }
    public GameObject AimCamera { get => aimCamera; set => aimCamera = value; }
    public GameObject Player { get => player; set => player = value; }
    public GameObject InteractButton { get => interactButton; set => interactButton = value; }
    public GameObject MainCamera { get => mainCamera; set => mainCamera = value; }
    public GameObject BlockEnabled { get => blockEnabled; set => blockEnabled = value; }
    public GameObject REnabled { get => rEnabled; set => rEnabled = value; }
    public GameObject DashEnabled { get => dashEnabled; set => dashEnabled = value; }
    public GameObject PlatformEnabled { get => platformEnabled; set => platformEnabled = value; }


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Player = GameObject.FindWithTag("Player");
        aimCamera = GameObject.FindWithTag("MainCamera");
        interactButton = GameObject.FindWithTag("InteractButton");
        MainCamera = GameObject.FindWithTag("MainCamera");
        REnabled = GameObject.FindWithTag("AttackR");
        BlockEnabled = GameObject.FindWithTag("BlockE");
        DashEnabled = GameObject.FindWithTag("DashQ");
        PlatformEnabled = GameObject.FindWithTag("PlatformE");
        EventManager = GameObject.FindWithTag("EventManager");


    }
    private void Start()
    {

        dontDestroyOnLoad = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DontDestroyOnLoad>();

        dontDestroyOnLoad.deactivateSceneToLoad(1);
        dontDestroyOnLoad.addToListScenesToUnload(SceneManager.UnloadSceneAsync("Intro"));
    }
    // Start is called before the first frame update
}
