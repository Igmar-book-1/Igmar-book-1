using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Geronimo Gorriarena
public class FallingRockController : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition;
    bool falling = false;
    Rigidbody _rb;
    public float maxSpeed = 5;
    public float acceleration = 2;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        _rb.AddForce(Vector3.back * 180, ForceMode.Impulse);
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
    public void CollisionDetected(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            


                collision.gameObject.transform.parent = transform;
            
            //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(Fall());

        }
    }

    public void CollisionDetectedStay(Collision collision)
    {
        if (collision.collider.tag == "Player")

            collision.gameObject.transform.parent = transform;
        //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        {
            if (!collision.gameObject.GetComponent<PlayerOneScript>().isMoving())
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * -17f, ForceMode.Force);
            }
            StartCoroutine(Fall());

        }
    }
    public void CollisionDetectedExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            StartCoroutine(Ascend());
            collision.gameObject.transform.parent = GameManager.instance.PlayerParent.transform;
            //collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
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
        _rb.AddForce(Vector3.back * acceleration, ForceMode.Force);
        //isPushed = false;
    }

}
