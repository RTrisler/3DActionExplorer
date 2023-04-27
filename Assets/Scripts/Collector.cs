using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private RaycastHit hit;
    private PlayerManager playerManager;

    [SerializeField]
    private LayerMask canCollect;

    private void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position - transform.forward * .5f, transform.forward * 1f, Color.green);
        if (Physics.Raycast(transform.position - transform.forward * .5f, transform.forward * 1f, out hit, 1f, canCollect))
        {
            if (hit.transform.TryGetComponent<IPickupable>(out IPickupable pickup))
            {
                pickup.collect(playerManager);
                Debug.Log(playerManager.score);
            }
        }
    }
}
