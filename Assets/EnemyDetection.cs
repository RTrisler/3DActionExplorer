using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{

    public FloatEnemy floatEnemy;

    void Start()
    {
        floatEnemy = GetComponentInParent<FloatEnemy>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            Debug.Log("Player is in Range");
            floatEnemy.PlayerDetected(player);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            Debug.Log("Player is out of Range");
            floatEnemy.PlayerUndetected(player);
        }
    }
}
