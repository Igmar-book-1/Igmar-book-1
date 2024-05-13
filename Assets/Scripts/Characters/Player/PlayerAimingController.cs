using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerAimingController : MonoBehaviour
{

    public PlayerOneScript player;

    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerOneScript>();
        aimReticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.getIsAiming() && !aimCamera.activeInHierarchy) || player.getIsDashing())
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
            player.SetisAiming(true);

            if (!player.getIsDashing())
            {
                aimReticle.SetActive(true);
                //StartCoroutine(ShowReticle());
            }

        }
        else if (!player.getIsAiming() && !mainCamera.activeInHierarchy)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
            player.SetisAiming(false);
        }

    }

    /*
    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.2f);
        aimReticle.SetActive(enabled);
    }*/

}
