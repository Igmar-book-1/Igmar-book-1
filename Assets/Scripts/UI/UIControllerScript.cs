using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{

    private PlayerOneScript _player;
    private GameObject manaBar;
    private GameObject healthBar;
    private Slider manaSlider;
    private Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerOneScript>();
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
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
