using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//TP2 - Matias Sueldo
public class animatorToRigidBody : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private SphereCollider sphereCollider;
    private PlayerOneScript playerOneScript;
    private Animator animator;
    [SerializeField] protected ParticleSystem particleSystem; 
    // Start is called before the first frame update
    void Start()
    {
        playerOneScript = this.GetComponentInParent<PlayerOneScript>();
        rb = this.GetComponentInParent<Rigidbody>();
        capsuleCollider = this.GetComponentInChildren<CapsuleCollider>();
        sphereCollider = this.GetComponentInChildren<SphereCollider>();
        animator = this.GetComponent<Animator>();
       
        stopParticleSystem();
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

    public void setRigidBodyVelocity(float drag)
    {
        playerOneScript.GetRigidBody().drag = drag;
    }

    public void turnOnMagicLayerWeight()
    {
        animator.SetLayerWeight(2, 0);
    }
    public void turnOffMagicLayerWeight()
    {
        animator.SetLayerWeight(2, 0);
    }

    public void enableParticleSystem()
    {
        particleSystem.Play();
    }
    public void disableParticleSystem()
    {

       StartCoroutine(disableparticlesSlow());
    }

    public void stopParticleSystem()
    {

        particleSystem.Stop();
    }

    IEnumerator disableparticlesSlow()
    {
        yield return new WaitForSeconds(0.5f);
        particleSystem.Stop();
    }


}
