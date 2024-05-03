using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTable : MonoBehaviour
{
    [SerializeField] float minDelayTime = 2;
    [SerializeField] float maxDelayTime = 5;

    private bool fallActivated = false;

    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es el jugador
        if (other.CompareTag("Player") && !fallActivated)
        {
            float delayTime = Random.Range(minDelayTime, maxDelayTime);

            Invoke("Fall", delayTime);

            fallActivated = true;
        }
    }

    private void Fall()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

    }
}
