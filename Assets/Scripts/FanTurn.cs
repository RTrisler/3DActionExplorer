using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTurn : MonoBehaviour
{
    [SerializeField]
    private GameObject _fan;

    private bool _turning;

    [SerializeField]
    private Animator _button;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            if (!_turning)
            {
                _button.SetBool("onButton", true);
                StartCoroutine(turn());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            _button.SetBool("onButton", false);
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
