using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPickup : PickUp
{
    //[SerializeField] private int charge = 20;
    public PlayerOneScript playerOneScript;

    public override void OnPickup()
    {
        playerOneScript.GetComponent<PlayerOneScript>().receiveMana();
        Destroy(gameObject);
    }
}
