using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerAimingController : MonoBehaviour
{

    public PlayerOneRigidBody player;

    public GameObject mainCamera;
    public GameObject aimCamera;
    //public GameObject aimReticle;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerOneRigidBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getIsAiming() && !aimCamera.activeInHierarchy)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);

            
           // StartCoroutine(ShowReticle());
        }
        else if (!player.getIsAiming() && !mainCamera.activeInHierarchy)
        {
             mainCamera.SetActive(true);
             aimCamera.SetActive(false);
             //aimReticle.SetActive(false);
        }
        
    }
    /*IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }*/

}
