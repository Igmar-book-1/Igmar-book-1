using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockBuildScripts : MonoBehaviour
{
    public Vector3 normal;
    public Vector3 initialPosition;
    public Vector3 nextPosition;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        Destroy(this.gameObject,7f);
    }
    private void Update()
    {


            transform.Translate(normal * Time.deltaTime, Space.World);

    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            normal = new Vector3(0,0,0);
        }
    }
}
