using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityToken : Interactable
{
    [SerializeField] protected GameObject abilityCanva;
    [SerializeField] protected GameObject abilityQ;
    [SerializeField] protected GameObject abilityR;
    [SerializeField] protected string ability;
    Animator animator;
    bool isActive;

    public override void Execute(Collider other)
    {
        isActive = false;
        if(ability == "Q")
        {
            abilityQ.SetActive(true);
            animator.Play("showQ");
        }
        else
        {
            abilityR.SetActive(true);
            animator.Play("showR");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        animator = abilityCanva.GetComponent<Animator>();
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            textMeshPro.SetText(texto);

            setCamera(GameManager.instance.MainCamera);

            interactButton.SetActive(true);
            interactButton.transform.position = transform.position + Vector3.up*5;
            if (camera != null)
            {
                interactButton.transform.LookAt(camera.transform);
            }
            interactButton.transform.Rotate(0, 0, 0);
        }
    }
}
