using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBall : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string _interactUi;

    [SerializeField]
    private Transform _ballLocation;

    [SerializeField]
    private GameObject _puzzleBall;
    public void Interact()
    {
        _puzzleBall.transform.position = _ballLocation.position;
        var _ballRigid = _puzzleBall.GetComponent<Rigidbody>();
        _ballRigid.velocity = new Vector3(0, 0, 0);
    }

    public string SendInteractUi()
    {
        return _interactUi;
    }
}
