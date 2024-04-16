using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneRigidBody  : CharacterController
{
    

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        base._xAxis = Input.GetAxis("Horizontal");
        base._zAxis = Input.GetAxis("Vertical");

        base._anim.SetFloat(base._xAxisName, base._xAxis);
        base._anim.SetFloat(base._zAxisName, base._zAxis);

        if (base._xAxis !=0 || base._zAxis != 0)
        {
            base.movement();
        }
    }

}
