using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

//TP2 - Matias Sueldo
public class StaffDamageScript : MonoBehaviour
{
    private PlayerOneScript playerOne;
    private DamageController damageController;
    [SerializeField] protected GameObject particleSystemObject;
    private bool Generated;


    // Start is called before the first frame update
    void Start()
    {
        this.playerOne = GameManager.instance.Player.GetComponent<PlayerOneScript>();
        this.damageController = playerOne.GetDamageController();
        particleSystemObject = Resources.Load<GameObject>("AttackParticles");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
     {
         if (other.tag == "Enemy")
         {
            //Debug.Log(other.name);
            if (!Generated)
            {
                damageController.OnHitEnemy(other);
                MonoBehaviour.Instantiate(particleSystemObject, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), Quaternion.identity);
                Generated = true;
                StartCoroutine(disable());
            }
        }
        //particleSystemObject.transform = other.ClosestPoint();

    }

    private IEnumerator disable()
    {
        yield return new WaitForSeconds(1f);
        Generated=false;
    }
}

