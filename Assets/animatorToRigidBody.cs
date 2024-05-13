using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class animatorToRigidBody : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private SphereCollider sphereCollider;
    private PlayerOneScript playerOneScript;
    // Start is called before the first frame update
    void Start()
    {
        playerOneScript = this.GetComponentInParent<PlayerOneScript>();
        rb = this.GetComponentInParent<Rigidbody>();
        capsuleCollider = this.GetComponentInChildren<CapsuleCollider>();
        sphereCollider = this.GetComponentInChildren<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestFunction()
    {
        //rb.AddForce(rb.gameObject.transform.forward * 2, ForceMode.Impulse);
        rb.AddForce(rb.gameObject.transform.forward * 7, ForceMode.Impulse);
    }

    public void ImpulseZ(float force)
    {
        //rb.AddForce(rb.gameObject.transform.forward * 2, ForceMode.Impulse);
        rb.AddForce(rb.gameObject.transform.forward * force, ForceMode.Impulse);
    }

    public void ImpulseY(float force)
    {
        //rb.AddForce(rb.gameObject.transform.forward * 2, ForceMode.Impulse);
        rb.AddForce(rb.gameObject.transform.up * force, ForceMode.Impulse);
    }

    public void AttackCollidersCapsule()
    {
            capsuleCollider.enabled = !capsuleCollider.enabled;
    }

    public void AttackCollidersSphere()
    {
        sphereCollider.enabled = !sphereCollider.enabled;
    }

    public void finishAttack()
    {
        capsuleCollider.enabled = false;
        sphereCollider.enabled = false;
        playerOneScript.setIsAttacking(false);
    }

    public void startCreatingBlocks()
    {
        playerOneScript.setIsCreatingPlatform(true);
    }
    public void finishCreatingBlocks()
    {
        playerOneScript.setIsCreatingPlatform(false);
    }
    public void SetIsDashingTrue()
    {
        playerOneScript.setIsDashing(true);
    }

    public void SetIsDashingFalse()
    {
        playerOneScript.setIsDashing(false);
    }
}
