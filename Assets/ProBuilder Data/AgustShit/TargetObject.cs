using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private void Awake()
    {
        UIControllerScript ui = GetComponentInParent<UIControllerScript>();
        if (ui == null)
        {
            ui = GameObject.Find("UI").GetComponent<UIControllerScript>();
        }

        if (ui == null) Debug.LogError("UI Controller component found");

        ui.AddTargetIndicator(this.gameObject);
    }

    
}
