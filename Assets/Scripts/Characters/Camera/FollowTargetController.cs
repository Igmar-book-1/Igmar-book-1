using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.AI;

//TP2 - Matias Sueldo
public class FollowTargetController : MonoBehaviour
{

    //private NaveMeshAgent _agent;
    public Vector2 _move;
    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;
    private PlayerOneScript player;
    [SerializeField] protected int maxAngle= 320;
    [SerializeField] protected int minAngle = 35;

    protected float _xAxis = 0;
    protected float _yAxis = 0;



    public Vector3 nextPosition;
    public Quaternion nextRotation;
    public float speed = 1f;

    private void Awake()
    {

        //Este agente es para que no se choque contra las paredes pero creo que no es necesario por ahora.
        //  _agent = GetComponent<NaveMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player.GetComponent<PlayerOneScript>();
    }


    public GameObject followTransform;
    public PlayerOneScript rigidBody;
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript._isPause && !player.getIsCreatingPlatform() && !player.IsDead)
        {
            _xAxis = Input.GetAxis("Mouse X");
            _yAxis = Input.GetAxis("Mouse Y") * -1;

            #region Follow transform rotation
            transform.rotation *= Quaternion.AngleAxis(_xAxis * rotationPower, Vector3.up);

            transform.rotation *= Quaternion.AngleAxis(_yAxis * rotationPower, Vector3.right);

            #endregion
            var angles = transform.transform.localEulerAngles;
            angles.z = 0;

            var angle = transform.localEulerAngles.x;

            if (angle > 180 && angle < maxAngle)
            {
                angles.x = maxAngle;
            }
            else if (angle < 180 && angle > minAngle)
            {
                angles.x = minAngle;
            }

            transform.localEulerAngles = angles;

            nextRotation = Quaternion.Lerp(transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

            if (followTransform.GetComponent<PlayerOneScript>().isMoving())
            {
                float moveSpeed = speed / 100f;
                Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
              
                nextPosition = followTransform.transform.position + position;

                followTransform.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                return;
            }


            nextPosition = transform.position;
            if (followTransform.GetComponent<PlayerOneScript>().getIsAiming())
            {
                followTransform.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

                transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }

        }

    }
}
