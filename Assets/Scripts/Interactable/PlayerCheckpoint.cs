using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Geronimo Gorriarena
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
