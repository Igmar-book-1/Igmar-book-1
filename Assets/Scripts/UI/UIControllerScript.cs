using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

//TP2 - Matias Sueldo
public class UIControllerScript : MonoBehaviour
{

    private PlayerOneScript _player;
    private GameObject manaBar;
    private GameObject healthBar;
    private Slider manaSlider;
    private Slider healthSlider;
    private GameObject rEnabled;
    private GameObject blockEnabled;
    private GameObject dashEnabled;
    private GameObject platformEnabled;

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
        rEnabled = GameManager.instance.REnabled;
        blockEnabled = GameManager.instance.BlockEnabled;
        dashEnabled = GameManager.instance.DashEnabled;
        platformEnabled = GameManager.instance.PlatformEnabled;
        dashEnabled.SetActive(false);
        rEnabled.SetActive(false);
        EventManager.OnAbilityEnabled += AbilityEnable;
}
    private void AbilityEnable(string ability)
    {
        if (ability == "R")
        {
            rEnabled.SetActive(true);
        }
        else if (ability == "Q")
        {
            dashEnabled.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        manaSlider.value = _player.getMana();
        healthSlider.value = _player.GetLife();

        if (targetIndicators.Count > 0)
        {
            for (int i = 0; i < targetIndicators.Count; i++)
            {
                targetIndicators[i].UpdateTargetIndicator();
                targetIndicators[i].turnOfTargetIndicator(_player.transform);
            }
        }

        if(_player.getIsAiming())
        {
            blockEnabled.gameObject.SetActive(false);
        }
        else
        {
            blockEnabled.gameObject.SetActive(true);
        }
        
        updateAllCooldowns();
    }


    private void updateAllCooldowns()
    {
            setCooldownSlider(rEnabled.transform.GetChild(0).GetComponentInChildren<Image>(), _player.getCurrentAttackRCooldown(), _player.getAttackRCooldown());
            setCooldownSlider(blockEnabled.transform.GetChild(0).GetComponentInChildren<Image>(), _player.getCurrentBlockECooldown(), _player.getBlockECooldown());
            setCooldownSlider(platformEnabled.transform.GetChild(0).GetComponentInChildren<Image>(), _player.getCurrentBlockECooldown(), _player.getBlockECooldown());
            setCooldownSlider(dashEnabled.transform.GetChild(0).GetComponentInChildren<Image>(), _player.getCurrentDashQCooldown(), _player.getDashQCooldown());
        

    }
    private void setCooldownSlider(Image image, float current, float max)
    {
        image.fillAmount=  1-(current/max);
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
    /*public GameObject getTargetIndicator(GameObject indicator)
    {
        return TargetIndicator;
    }*/

}
