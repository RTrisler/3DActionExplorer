using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private RaycastHit hit;
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * .05f, Color.green);
        if (Physics.Raycast(transform.position, transform.forward * .05f, out hit, .05f))
        {
            if (hit.transform.TryGetComponent<IPickupable>(out IPickupable pickup))
            {
                pickup.collect(playerManager);
                Debug.Log(playerManager.score);
            }
        }
    }
}
