using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerOneMovement : MonoBehaviour
{
    [SerializeField] int playerLife = 3;
    [SerializeField] int playerHealth = 100;
    [SerializeField] float playerMovementSpeed = 5f;
    [SerializeField] float playerJumpForce = 10f;
    private float _zAxis;
    private float _xAxis;
    private Vector3 _dir = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
      

    }


}
