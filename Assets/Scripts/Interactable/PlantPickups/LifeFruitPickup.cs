using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Matias Sueldo
public class LifeFruitPickup : MonoBehaviour, Fruit 
{
    [SerializeField] protected int life = 100;
    [SerializeField] protected int mana = 0;
    [SerializeField] protected int armor = 0;
    [SerializeField] protected int velocity = 0;

    public void execute(Collider player)
    {
        player.GetComponent<PlayerOneScript>().cure(life);
    }
}
