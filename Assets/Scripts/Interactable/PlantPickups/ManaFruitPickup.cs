using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFruitPickup : MonoBehaviour, Fruit
{
    [SerializeField] protected int life = 0;
    [SerializeField] protected int mana = 20;
    [SerializeField] protected int armor = 0;
    [SerializeField] protected int velocity = 0;

    public void execute(Collider player)
    {
        player.GetComponent<PlayerOneScript>().receiveMana(mana);
    }
}
