using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected GameObject interactButton;
    protected GameObject camera;
    public TMP_Text textMeshPro;
    [SerializeField] protected string texto;

    public virtual void Start()
    {
        interactButton = GameManager.instance.InteractButton;
        interactButton.SetActive(false);
        setCamera(GameManager.instance.MainCamera);
        textMeshPro = interactButton.GetComponentInChildren<TMP_Text>();
        textMeshPro.SetText(texto);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            setCamera(GameManager.instance.MainCamera);
            interactButton.SetActive(true);
            interactButton.transform.position = transform.position + Vector3.up;
            interactButton.transform.LookAt(camera.transform);
            interactButton.transform.Rotate(0, 0, 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (camera != null)
        {
            interactButton.transform.LookAt(camera.transform);
            interactButton.transform.Rotate(90, 0, 0);
        }


        if (other.tag=="Player" && Input.GetKeyDown(KeyCode.F))
        {
            Execute(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactButton.SetActive(false);
        }
    }

    public abstract void Execute(Collider other);

    //public override void OnPickup()
    //{
      //  playerOneScript.GetComponent<PlayerOneScript>().receiveMana();
        //Destroy(gameObject);
    //}

    public void setCamera(GameObject camera2)
    {
        camera = camera2;
    }
    public GameObject getCamera()
    {
        return this.camera;
    }
}
