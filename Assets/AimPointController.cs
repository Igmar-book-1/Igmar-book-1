using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPointController : MonoBehaviour
{
    [SerializeField] GameObject camera;
    private GameObject aimPoint;
    private PlayerOneScript player;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.AimCamera!= null)
        {
            camera = GameManager.instance.AimCamera;
        }
        aimPoint = GameObject.FindWithTag("AimPoint");
        player = GameManager.instance.Player.GetComponent<PlayerOneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.getIsAiming())
        {
            this.GetComponentInParent<MeshRenderer>().enabled = false;
        }
        aimPoint = GameObject.FindWithTag("AimPoint");
        if (GameManager.instance.AimCamera != null && player.getIsAiming()) {
            this.GetComponentInParent<MeshRenderer>().enabled = true;
            this.transform.LookAt(camera.transform);
            this.transform.Rotate(90, 0, 0);
            if (aimPoint == null)
            {
                aimPoint = GameObject.FindWithTag("AimPoint");
            }
            else
            {
                Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(aimPoint.transform.position);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit))
                {
                    GameObject targetHit = rayHit.transform.gameObject;
                    Vector3 hitPosition = rayHit.point;
                    this.transform.position = hitPosition;
                }
            }
        }
    }
        
}
