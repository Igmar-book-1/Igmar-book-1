using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockBuildScripts : MonoBehaviour
{
    public Vector3 normal;
    public Vector3 initialPosition;
    public Vector3 nextPosition;
    bool isSet = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        Destroy(this.gameObject,14f);
    }
    private void Update()
    {


            StartCoroutine(CreateRock());

    }

    private void OnCollisionExit(Collision collision)
    {
    }


    IEnumerator CreateRock()
    {
        if (tag == "Platform" || tag == "Piedra")
        {
            yield return new WaitForSeconds(1f);
        }


        if (tag == "Platform" &&Vector3.Distance(transform.position, initialPosition) > 2 
            || (tag == "Piedra" && Vector3.Distance(transform.position, initialPosition) > 2.8) ||
             (tag == "Escudo" && Vector3.Distance(transform.position, initialPosition) > 2.9))
        {
            yield break;
        }
        isSet = true;

        if(tag== "Escudo")
        {
            transform.Translate(Vector3.up * Time.deltaTime * 6, Space.World);
        }
        else
        {
            transform.Translate(normal * Time.deltaTime * 6, Space.World);
        }
    }

}
