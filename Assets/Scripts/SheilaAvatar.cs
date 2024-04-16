using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheilaAvatar : MonoBehaviour
{

    private CharacterController _parent;
    // Start is called before the first frame update
    void Start()
    {
        _parent = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
