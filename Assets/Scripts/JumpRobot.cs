using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRobot : MonoBehaviour, IInteractable
{
    private BossState _currentState;

    [SerializeField]
    private string _interactUi;

    [SerializeField]
    private Transform _jumpSpot;

    [SerializeField]
    private List<Transform> _moveLocations;
    void Start()
    {
        _currentState = BossState.Idle;
    }

    void Update()
    {
        switch (_currentState)
        {
            case BossState.Idle:
                break;
            case BossState.Attacking:
                break;
            case BossState.Resting:
                break;
        }
    }

    private void EnterState(BossState currentState)
    {
        switch (_currentState)
        {
            case BossState.Idle:
                break;
            case BossState.Attacking:
                this.transform.position = _moveLocations[Random.Range(0, _moveLocations.Count)].position;
                break;
            case BossState.Resting:
                break;
        }
    }

    public void Interact()
    {
        _currentState = BossState.Attacking;
    }

    public string SendInteractUi()
    {
        return _interactUi;
    }
}
public enum BossState
{
    Idle,
    Attacking,
    Resting
}
