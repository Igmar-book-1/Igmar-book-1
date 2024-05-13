using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class StaffDamageScript : MonoBehaviour
{
    private PlayerOneScript playerOne;
    private DamageController damageController;

    // Start is called before the first frame update
    void Start()
    {
        this.playerOne = GameManager.instance.Player.GetComponent<PlayerOneScript>();
        this.damageController = playerOne.GetDamageController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log(other.name);
            damageController.OnHitEnemy(other);
        }
        
    }

}
