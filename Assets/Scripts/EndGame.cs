using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private BoxCollider _endCOllide;

    private void Start()
    {
        _endCOllide = GetComponent<BoxCollider>();
        _endCOllide.enabled = false;
    }

    private void OnEnable()
    {
        NewSTEPManager.OnAllSTEP += enableEndCollide;
    }
    private void OnDisable()
    {
        NewSTEPManager.OnAllSTEP -= enableEndCollide;
    }

    private void enableEndCollide()
    {
        _endCOllide.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            player.SetScore(player.score);
            player.sceneManager.MoveToScene(2);
        }
    }
}
