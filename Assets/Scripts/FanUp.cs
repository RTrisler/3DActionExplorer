using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanUp : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PuzzleBall>(out PuzzleBall ball))
        {
            ball._ballRigidBody.AddForce(-1 * (this.transform.up * 20));
        }
    }
}
