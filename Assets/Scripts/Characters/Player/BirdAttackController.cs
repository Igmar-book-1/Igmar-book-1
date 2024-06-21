using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        animator.Play("Macaw_|Flight");
        current = posA;
        target = posB;
        transform.position = current;
        rb.AddForce(( target-current) * speed *Time.deltaTime);
        //StartCoroutine(WaitAndMove(0));

    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position != target)
        //{
          //  sinTime += Time.deltaTime * movementSpeed;
            //sinTime = Mathf.Clamp(sinTime,0,Mathf.PI);
            //float t = evaluate(sinTime);

            //transform.position = Vector3.Lerp(current, target, t);
        //}
    }

    private float evaluate(float sinTime)
    {
        return 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2f) + 0.5f;
    }

    IEnumerator WaitAndMove(float delayTime)
    {
        //yield return new WaitForSeconds(delayTime); // start at time X
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Vector3.Distance(posA,posB) >= 0.2)
        { // until one second passed
            transform.position = Vector3.Lerp(posA, posB, (Time.time -startTime) *0.05f); // lerp from A to B in one second
            //yield return new WaitForSeconds(0.1f);
            yield return null; // wait for next frame
        }
    }
}
