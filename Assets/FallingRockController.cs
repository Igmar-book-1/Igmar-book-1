using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockController : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition;
    bool falling = false;
    Rigidbody _rb;
    public float maxSpeed = 10;
    public float acceleration = 8;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        _rb.AddForce(Vector3.left * 180, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 60f);
       
    }

    private void FixedUpdate()
    {
        if(_rb.velocity.magnitude< maxSpeed)
        {
            PushRockForward();
        }
        if (falling)
        {
            PushRockUp(-2);
        }
        else if (transform.position.y < initialPosition.y)
        {
            PushRockUp(1);

        }
        if (transform.position.y> initialPosition.y)
        {
            PushRockUp(-1);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            StartCoroutine(Fall());

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            StartCoroutine(Ascend());
        }

    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(3f);
        falling = true;
    }

    IEnumerator Ascend()
    {
        yield return new WaitForSeconds(2f);
        falling = false;
    }

    void PushRockUp(float positivity)
    {
        _rb.AddForce(Vector3.up * positivity * 8, ForceMode.Force); 
    }
    void PushRockForward()
    {
        //isPushed = true;
        _rb.AddForce(Vector3.left * acceleration, ForceMode.Force);
        //isPushed = false;
    }

}
