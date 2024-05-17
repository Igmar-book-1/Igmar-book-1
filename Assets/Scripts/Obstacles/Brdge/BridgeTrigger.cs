using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] bridgeTables;
    [SerializeField] float minDelayTime = 5f; // Para caida de los tablones
    [SerializeField] float maxDelayTime = 15f; // PAra caida de los tablones

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
           // Debug.Log("Trigger activado");

            foreach (GameObject bridgeTable in bridgeTables)
            {
                // Activa el tablón y comienza la vibración y caída en un tiempo aleatorio
                BridgeTable bridgeTableScript = bridgeTable.GetComponent<BridgeTable>();
                float delayTime = Random.Range(minDelayTime, maxDelayTime);

                bridgeTableScript.ActivateFall(delayTime, delayTime-3);
            }
        }
    }
}