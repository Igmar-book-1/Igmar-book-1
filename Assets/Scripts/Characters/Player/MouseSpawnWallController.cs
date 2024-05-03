using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawnWallController : MonoBehaviour
{
    private Camera cam;
    private GameObject obj;
    private GameObject obj2;
    private GameObject aimPoint;
    private PlayerOneScript player;

    // Start is called before the first frame update
    void Start()
    {
        obj = Resources.Load<GameObject>("Platform");
        obj2 = Resources.Load<GameObject>("Piedra");
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        aimPoint = GameObject.FindWithTag("AimPoint");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerOneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && player.getIsAiming())
        {
            Ray ray = cam.ScreenPointToRay(aimPoint.transform.position);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                GameObject targetHit = rayHit.transform.gameObject;
                Vector3 hitPosition = rayHit.point;
                
                if (targetHit != null)
                {
                    
                    if (Vector3.Dot(rayHit.normal,Vector3.up)<0.6)
                    {
                        //hitPosition = hitPosition + Vector3.up * obj.transform.localScale.y / 2;
                        Instantiate(obj, hitPosition, Quaternion.identity);
                    } else
                    {
                        Instantiate(obj2, hitPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}
