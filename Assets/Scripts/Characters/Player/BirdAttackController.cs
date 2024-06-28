using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TP2 - Matias Sueldo
public class BirdAttackController : MonoBehaviour
{
    public float delayTime;
    public Vector3 posA;
    public Vector3 posB;

    public float movementSpeed;
    private Vector3 current;
    private Vector3 target;
    private float sinTime;
    public Rigidbody rb;
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    [SerializeField] int damage;
    private Collider collider;
    private string currentAnimation;
    private bool stopped;
    BirdSoundController soundController;
    GameObject collisionParticles;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        soundController = GetComponent<BirdSoundController>();
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        collisionParticles = Resources.Load<GameObject>("FeatherParticles");
        rb = GetComponent<Rigidbody>();
        animator.Play("Macaw_|Flight");
        currentAnimation = "Macaw_|Flight";
        current = posA;
        target = posB;
        transform.position = current;
        rb.AddForce(( target-current).normalized * speed *200*Time.deltaTime);

        stopped = false;
        Destroy(this.gameObject, 10f);

        //StartCoroutine(WaitAndMove(0));

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 4 && currentAnimation != "Macaw_|Flying_attack")
        {
            soundController.playBirdAttackSound();
            animator.CrossFade("Macaw_|Flying_attack", 0.5f, -1, 0.5f);
            currentAnimation = "Macaw_|Flying_attack";
    
        }

        if (Vector3.Distance(transform.position, target)<3&& !stopped)
        {
            animator.Play("Macaw_|Flying_attack");


        }
        if (Vector3.Distance(transform.position, target) < 0.2 && !stopped)
        {
            rb.velocity = (target - current).normalized * 1 * Time.deltaTime;
            stopped = true;
            StartCoroutine(flyAway(1f));
        }


        //{
        //  sinTime += Time.deltaTime * movementSpeed;
        //sinTime = Mathf.Clamp(sinTime,0,Mathf.PI);
        //float t = evaluate(sinTime);

        //transform.position = Vector3.Lerp(current, target, t);
        //}
    }


    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Enemy")
        {
            this.collider.enabled = false;
            animator.Play("Macaw_|Flying_attack");
            rb.velocity = (target - current).normalized * 1 * Time.deltaTime;
            stopped = true;
            GameObject Instance = MonoBehaviour.Instantiate(collisionParticles, collision.ClosestPoint(transform.position), Quaternion.identity);
            Destroy(Instance, 3f);
            StartCoroutine(flyAway(1f));
            collision.GetComponent<AllCharacterController>().ReceiveDamage(20);

        }

    }

    private IEnumerator flyAway(float time)
    {
        yield return new WaitForSeconds(time);
        animator.Play("Macaw_|Flight");
        currentAnimation = "Macaw_|Flight";
        rb.AddForce((transform.forward) * speed * 100 * Time.deltaTime+ Vector3.up * speed *100 * Time.deltaTime);
    }
}