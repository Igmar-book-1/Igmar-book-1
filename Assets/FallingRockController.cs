using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockController : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition;
    bool isPushed=false;
    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        _rb.AddForce(Vector3.left * 20, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 30f);
        PushRockForward();
        if(!isPushed && this.transform.position.y!= initialPosition.y)
        {
            if (this.transform.position.y > initialPosition.y)
            {
                PushRockUp(-1);
            }
            else
            {
                PushRockUp(1);
            }
        }
    }

    void PushRockUp(float positivity)
    {
        isPushed = true;
        _rb.AddForce(Vector3.up * positivity * 8, ForceMode.Force);
        isPushed = false;
    }
    void PushRockForward()
    {
        isPushed = true;
        _rb.AddForce(Vector3.left * 4, ForceMode.Force);
        isPushed = false;
    }

}
