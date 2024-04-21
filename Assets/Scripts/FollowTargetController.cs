using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.AI;

public class FollowTargetController : MonoBehaviour
{

    //private NaveMeshAgent _agent;
    public Vector2 _move;
    public Vector2 _look;
    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    protected float _xAxis = 0;
    protected float _yAxis = 0;



    public Vector3 nextPosition;
    public Quaternion nextRotation;
    public float speed = 1f;
    public Camera camera;

    private void Awake()
    {
        //Este agente es para que no se choque contra las paredes pero creo que no es necesario por ahora.
      //  _agent = GetComponent<NaveMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public GameObject followTransform;
    // Update is called once per frame
    void Update()
    {

        _xAxis = Input.GetAxis("Mouse X");
        _yAxis = Input.GetAxis("Mouse Y");

        #region Follow transform rotation
        transform.rotation *= Quaternion.AngleAxis(_xAxis * rotationPower, Vector3.up);

        transform.rotation *= Quaternion.AngleAxis(_yAxis * rotationPower, Vector3.right);

        followTransform.transform.rotation *= Quaternion.AngleAxis(_xAxis * rotationPower, Vector3.up);
        #endregion
        var angles = transform.transform.localEulerAngles;
        angles.z = 0;

        var angle = transform.localEulerAngles.x;

        if (angle > 180 && angle < 355)
        {
            angles.x = 355;
        }
        else if (angle < 180 && angle > 10)
        {
            angles.x = 10;
        }

        transform.localEulerAngles = angles;

        nextRotation = Quaternion.Lerp(transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        if( _move.x == 0 && _move.y == 0)
        {
            nextPosition = transform.position;
            /*if(aimValue == 1)
            {
                transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }*/

            return;
        }
           

    }
}
