using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBall : MonoBehaviour
{
    public Rigidbody _ballRigidBody;

    [SerializeField]
    public SphereCollider _collectCollider;

    private void Start()
    {
        _ballRigidBody = GetComponent<Rigidbody>();
        _collectCollider.enabled = false;
    }
}
