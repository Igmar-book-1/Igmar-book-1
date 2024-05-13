using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControllerScript : MonoBehaviour
{

    private PlayerOneScript _player;
    private GameObject manaBar;
    private GameObject healthBar;
    private Slider manaSlider;
    private Slider healthSlider;

    public Canvas canvas;

    public List<TargetIndicator> targetIndicators = new List<TargetIndicator>();

    public Camera MainCamera;

    public GameObject TargetIndicatorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance.Player.GetComponent<PlayerOneScript>();
        manaSlider = GameObject.FindWithTag("Mana Bar").GetComponent<Slider>();
        healthSlider = GameObject.FindWithTag("Health Bar").GetComponent<Slider>();


    }

    // Update is called once per frame
    void Update()
    {
        manaSlider.value = _player.getMana();
        healthSlider.value = _player.GetLife();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("PreEntrega");
        }

        if (targetIndicators.Count > 0)
        {
            for (int i = 0; i < targetIndicators.Count; i++)
            {
                targetIndicators[i].UpdateTargetIndicator();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    

    

    public void AddTargetIndicator(GameObject target)
    {
        TargetIndicator indicator = GameObject.Instantiate(TargetIndicatorPrefab, canvas.transform).GetComponent<TargetIndicator>();
        indicator.InitialiseTargetIndicator(target, MainCamera, canvas);
        targetIndicators.Add(indicator);
    }
}
