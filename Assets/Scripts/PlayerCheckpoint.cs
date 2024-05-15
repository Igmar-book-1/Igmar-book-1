using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] Vector3 lastCheckpoint;

    private void Awake()
    {
        lastCheckpoint = transform.position;
    }

    public void SaveCheckpoint(Vector3 checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    public void LoadCheckpoint()
    {
        transform.position = lastCheckpoint;
    }
}
