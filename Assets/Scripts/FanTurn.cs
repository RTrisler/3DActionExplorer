using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTurn : MonoBehaviour
{
    [SerializeField]
    private GameObject _fan;

    private bool _turning;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            if (!_turning)
            {
                StartCoroutine(turn());
            }
        }
    }
    IEnumerator turn()
    {
        _turning = true;
        _fan.transform.Rotate(0, 90, 0);
        yield return new WaitForSeconds(.5f);
        _turning = false;
    }
}
