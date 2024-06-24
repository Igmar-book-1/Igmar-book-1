using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class PlantPickup : Interactable
{
    [SerializeField] protected string plantType;
    [SerializeField] protected GameObject fruitprefab;
    [SerializeField] protected GameObject fruit;
    int counter = 0;
    private bool counterWasCalled = false;

    private Transform spawnPoint;
    private void Start()
    {

        base.Start();
        textMeshPro.SetText(texto);
        spawnPoint = this.gameObject.transform.GetChild(0);
        fruitprefab = Resources.Load<GameObject>(plantType + "Fruit");
        fruit = MonoBehaviour.Instantiate(fruitprefab, spawnPoint.transform.position, Quaternion.identity);

    }

    private void Update()
    {
        if (fruit == null && !counterWasCalled)
        {
            StartCoroutine(updateCounter());
        }
    }

    IEnumerator updateCounter()
    {
        counterWasCalled = true;
        if (counter == 3)
        {
            fruit = MonoBehaviour.Instantiate(fruitprefab, spawnPoint.transform.position, Quaternion.identity);
            counter = 0;
            counterWasCalled= false;
            yield break;
        }
        yield return new WaitForSeconds(1);
        counter++;
        StartCoroutine(updateCounter());
    }
    /*
    IEnumerator updateCounter()
    {
        counterWasCalled = true;
        //if(counter==0)
        //{
        fruit = MonoBehaviour.Instantiate(fruitprefab, spawnPoint.transform.position, Quaternion.identity);
        //fruit.GetComponentInChildren<Transform>().transform.localScale = new Vector3(0, 0, 0);
        //}
        if (counter == 3)
        {
            fruit = MonoBehaviour.Instantiate(fruitprefab, spawnPoint.transform.position, Quaternion.identity);

            //fruit.GetComponentInChildren<Transform>().transform.localScale += new Vector3(67.296f / 3f, 67.296f / 3f, 67.296f / 3f);
            eateable = true;
            counter = 0;
            counterWasCalled = false;
            yield break;
        }
        yield return new WaitForSeconds(1);
        fruit.GetComponentInChildren<Transform>().transform.localScale += new Vector3(67.296f / 3f, 67.296f / 3f, 67.296f / 3f);
        counter++;
        StartCoroutine(updateCounter());
    }*/
    //private void OnTriggerEnter(Collider other)
    //{
    //if (other.tag == "Player")
    //{


    //interactButton.SetActive(true);
    //interactButton.transform.position = transform.position + Vector3.up;
    //interactButton.transform.LookAt(Camera.transform);
    //interactButton.transform.Rotate(0, 0, 0);
    //if (other.GetComponent<PlayerOneScript>() != null)
    //{
    //  OnPickup();
    //}
    //  }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //  base.OnTriggerStay(other);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //   if (other.tag == "Player")
    // {
    //   interactButton.SetActive(false);
    //}

    //}

    public override void Execute(Collider other)
    {
        if(fruit!=null)
        {
            fruit.transform.GetChild(0).GetComponentInChildren<Fruit>().execute(other);

            Destroy(fruit);
        }
    }


}
