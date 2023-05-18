using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewSTEPManager : MonoBehaviour
{
    public static event Action OnAllSTEP;
    private PlayerManager _playerManager;
    private PlayerLocomotion _playerMovement;
    private InputManager _playerInput;
    private PlayerCombat _playerCombat;

    private bool _S, _T, _E, _P = false;
    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        _playerMovement = GetComponent<PlayerLocomotion>();
        _playerInput = GetComponent<InputManager>();
        _playerCombat = GetComponent<PlayerCombat>();
    }
    private void OnEnable()
    {
        ScoreCollect.OnSTEPCollect += alterStep;
    }
    private void OnDisable()
    {
        ScoreCollect.OnSTEPCollect -= alterStep;
    }

    private void alterStep(STEP addedStep)
    {
        switch (addedStep)
        {
            case STEP.S:
                _playerMovement.SpeedUpMovement();
                _playerInput._canSprint = true;
                _S = true;
                Debug.Log("S collected");
                break;
            case STEP.T:
                _playerMovement._extendedJump = true;
                _playerInput._extendedJump = true;
                _T = true;
                Debug.Log("T collected");
                break;
            case STEP.E:
                _playerManager._canInteractSpecial = true;
                Debug.Log("E collected");
                _E = true;
                break;
            case STEP.P:
                Debug.Log("P collected");
                _playerCombat.attackDamage += 25;
                _P = true;
                break;
            default:
                break;
        }
        checkAllSTEP();
    }

    private void checkAllSTEP()
    {
        if(_S == true && _T == true && _E == true && _P == true)
        {
            OnAllSTEP?.Invoke();
        }
    }
}
public enum STEP
{
    None,
    S,
    T,
    E,
    P
}
