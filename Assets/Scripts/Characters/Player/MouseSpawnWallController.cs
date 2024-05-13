using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseSpawnWallController
{
    private Camera cam;
    private GameObject obj;
    private GameObject obj2;
    private GameObject obj3;
    private GameObject aimPoint;
    private PlayerOneScript player;
    private RaycastHit rayhitPosition;

    // Start is called before the first frame update
    public void Start()
    {
        obj = Resources.Load<GameObject>("Platform");
        obj2 = Resources.Load<GameObject>("Piedra");
        obj3 = Resources.Load<GameObject>("Escudo");
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        aimPoint = GameObject.FindWithTag("AimPoint");
        player = GameManager.instance.Player.GetComponent<PlayerOneScript>();
    }

    // Update is called once per frame
    public void CreateWall()
    {
        if (aimPoint == null)
        {
            aimPoint = GameObject.FindWithTag("AimPoint");
        }

        Ray ray = cam.ScreenPointToRay(aimPoint.transform.position);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit))
        {
            GameObject targetHit = rayHit.transform.gameObject;
            Vector3 hitPosition = rayHit.point;
            rayhitPosition = rayHit;
            if (!player.getIsCreatingPlatform())
            {
                GameObject instance;
                if (Vector3.Dot(rayHit.normal, Vector3.up) < 0.6)
                {
                    player.setOnCreateWall();
                    hitPosition = hitPosition - rayHit.normal * obj.transform.localScale.y / 100;
                    instance = MonoBehaviour.Instantiate(obj, hitPosition, Quaternion.identity);
                }
                else
                {
                    player.setOnCreateGround();

                    hitPosition = hitPosition - rayHit.normal * obj.transform.localScale.y / 70;
                    instance = MonoBehaviour.Instantiate(obj2, hitPosition, Quaternion.identity);

                }
                instance.GetComponent<RockBuildScripts>().normal = rayHit.normal;
            }
        }
    }


    public void CreateBlockingWall()
    {

        if (!player.getIsCreatingPlatform())
        {
            GameObject instance;
            instance = MonoBehaviour.Instantiate(obj3, player.transform.position + player.transform.forward - Vector3.up * 3, Quaternion.LookRotation(player.transform.forward));
        }

    }

    public RaycastHit getRayHit()
    {
        return rayhitPosition;
    }
}
