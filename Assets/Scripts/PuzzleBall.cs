using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBall : MonoBehaviour
{
    public Rigidbody _ballRigidBody;

    private void Start()
    {
        _ballRigidBody = GetComponent<Rigidbody>();
    }
}
