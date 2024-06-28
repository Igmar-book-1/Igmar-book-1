using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollisionDetectorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        transform.parent.GetComponent<FallingRockController>().CollisionDetected(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        transform.parent.GetComponent<FallingRockController>().CollisionDetectedStay(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        transform.parent.GetComponent<FallingRockController>().CollisionDetectedExit(collision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
