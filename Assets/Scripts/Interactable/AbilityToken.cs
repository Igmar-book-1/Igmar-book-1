using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Matias Sueldo
public class AbilityToken : Interactable
{
    [SerializeField] protected GameObject abilityCanva;
    [SerializeField] protected GameObject abilityQ;
    [SerializeField] protected GameObject abilityR;
    [SerializeField] protected GameObject abilityAiming;
    [SerializeField] protected string ability;
    ParticleSystem particleSystem;
    bool cooldown;
    Animator animator;
    bool isActive;

    public override void Execute(Collider other)
    {
        if (cooldown != true && !abilityCanva.activeInHierarchy)
        {
            cooldown = true;
            if (isActive == true)
            {
                abilityCanva.SetActive(true);
                isActive = false;
                if (ability == "Q")
                {
                    abilityQ.SetActive(true);
                    animator.Play("showQ");
                }
                else if(ability == "R")
                {
                    abilityR.SetActive(true);
                    animator.Play("showR");
                }
                else if (ability == "Aiming")
                {
                    abilityAiming.SetActive(true);
                    animator.Play("showAiming");
                }
                other.gameObject.GetComponent<PlayerOneScript>().enableAbility(ability);
                
            }
            StartCoroutine(cooldownUpdate());
            /*
            else
            {
                if (ability == "Q")
                {
                    animator.Play("hideQ");
                }
                else
                {
                    animator.Play("hideR");
                }
                isActive = true;
                StartCoroutine(disappear());
            }*/

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        animator = abilityCanva.GetComponent<Animator>();
        isActive = true;
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }


    IEnumerator cooldownUpdate()
    {
        yield return new WaitForSeconds(1);
        cooldown = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (cooldown != true && Input.GetKeyDown(KeyCode.F) && abilityCanva.activeInHierarchy)
        {
            cooldown = true;
            if (isActive != true)
            {
                if (ability == "Q")
                {
                    animator.Play("hideQ");
                }
                else if (ability == "R")
                {
                    animator.Play("hideR");
                }
                else if (ability =="Aiming")
                {
                    animator.Play("hideAiming");
                }
                particleSystem.Stop();
                isActive = true;
                StartCoroutine(disappear());
                StartCoroutine(cooldownUpdate());
            }
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cooldown = false;
            textMeshPro.SetText(texto);

            setCamera(GameManager.instance.MainCamera);

            interactButton.SetActive(true);
            interactButton.transform.position = transform.position + Vector3.up*4;
            if (camera != null)
            {
                interactButton.transform.LookAt(camera.transform);
            }
            interactButton.transform.Rotate(0, 0, 0);
        }
    }

    IEnumerator disappear()
    {
        yield return new WaitForSeconds(1);
        abilityCanva.SetActive(false);
        abilityQ.SetActive(false);
        abilityR.SetActive(false);
        abilityAiming.SetActive(false);
    }

}
