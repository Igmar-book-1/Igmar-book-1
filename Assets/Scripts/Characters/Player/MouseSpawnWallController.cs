using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseSpawnWallController
{
    private Camera cam;
    private GameObject obj;
    private GameObject obj2;
    private GameObject aimPoint;
    private PlayerOneScript player;

    // Start is called before the first frame update
    public void Start()
    {
        obj = Resources.Load<GameObject>("Platform");
        obj2 = Resources.Load<GameObject>("Piedra");
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        aimPoint = GameObject.FindWithTag("AimPoint");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerOneScript>();
    }

    // Update is called once per frame
    public void CreateWall()
    {
        if(aimPoint == null)
        {
            aimPoint = GameObject.FindWithTag("AimPoint");
        }
            Ray ray = cam.ScreenPointToRay(aimPoint.transform.position);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                GameObject targetHit = rayHit.transform.gameObject;
                Vector3 hitPosition = rayHit.point;
                
                if (targetHit != null)
                {

                GameObject instance;
                hitPosition = hitPosition - rayHit.normal * obj.transform.localScale.y / 100;
                if (Vector3.Dot(rayHit.normal,Vector3.up)<0.6)
                    {
                    instance = MonoBehaviour.Instantiate(obj, hitPosition, Quaternion.identity);
                    } else
                    {
                    instance = MonoBehaviour.Instantiate(obj2, hitPosition, Quaternion.identity);
                    
                    }
                instance.GetComponent<RockBuildScripts>().normal = rayHit.normal;
                }
            }
        
    }
}
