using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{
    [SerializeField]
    private float _pushForce;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<PlayerLocomotion>(out PlayerLocomotion locomotion))
        {
            locomotion.playerRigidBody.AddForce(this.transform.forward * _pushForce);
        }
    }
}
