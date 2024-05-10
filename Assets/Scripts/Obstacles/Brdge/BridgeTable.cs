using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTable : MonoBehaviour
{
    [SerializeField] float minDelayTime = 5f; //Debe coincidir con el min delay del trigger para optimo.
    [SerializeField] float maxDelayTimeOnStep = 7f;

    [SerializeField] float vibrationStrenght = 0.05f;
    
    [SerializeField] Color flashColor = Color.white;
    [SerializeField] float flashDuration = 1f;

    private bool activated = false;
    private bool fallActivated = false;

    private Material material;
    private Color originalColor;

    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;

        originalColor = material.color;
    }

    //METODO AL PISARLO, PARA LAS QUE NO ESTEN EN EL ARRAY
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es el jugador y si no fue activado aun
        if (other.CompareTag("Player") && !fallActivated)
        {
            float delayTime = Random.Range(minDelayTime, maxDelayTimeOnStep);

            Invoke("Fall", delayTime);

            float vibrationStartTime = delayTime - minDelayTime; //Para que empiece a vibrar al minDelayTime antes de que caiga
            InvokeRepeating("Vibrate", vibrationStartTime, 0.1f);

            //InvokeRepeating("Flash", vibrationStartTime, flashDuration);

            fallActivated = true;
        }
    } 

    //METODO DESDE BRIDGETRIGGER
    public void ActivateFall(float delayTime, float vibrationStartTime) //Aca podriamos hacerlo accesible desde el trigger sin que sea public?
    {
        if (!activated)
        {
            activated = true;
            Invoke("Fall", delayTime);

            InvokeRepeating("Vibrate", vibrationStartTime, 0.1f);

            //InvokeRepeating("Flash", vibrationStartTime, flashDuration); //Para que haga flash, no funcion muy bien.

            fallActivated = true;
        }
    }

    private void Vibrate()
    {
        Vector3 verticalDirection = new Vector3(0f, 1f, 0f);
        float verticalDisplacement = Random.Range(-vibrationStrenght, vibrationStrenght);
        transform.position += verticalDirection * verticalDisplacement;
    }

    private void Flash()
    {
        material.color = flashColor;
        Invoke("RestoreColor", flashDuration);
    }

    private void RestoreColor()
    {
        material.color = originalColor;
    }
    private void Fall()
    {
        //Para que deje de vibrar
        CancelInvoke("Vibrate");
        
        //Inicia caida
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
