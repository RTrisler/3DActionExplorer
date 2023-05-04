using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSTEPManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private PlayerLocomotion _playerMovement;
    private InputManager _playerInput;
    private PlayerCombat _playerCombat;
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
                Debug.Log("S collected");
                break;
            case STEP.T:
                _playerMovement._extendedJump = true;
                Debug.Log("T collected");
                break;
            case STEP.E:
                _playerManager._canInteractSpecial = true;
                Debug.Log("E collected");
                break;
            case STEP.P:
                Debug.Log("P collected");
                _playerCombat.attackDamage += 25;
                break;
            default:
                break;
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
