using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSTEPManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private PlayerLocomotion _playerMovement;
    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        _playerMovement = GetComponent<PlayerLocomotion>();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void alterStep(STEP addedStep)
    {
        switch (addedStep)
        {
            case STEP.S:
                _playerMovement.SpeedUpMovement();
                break;
            case STEP.T:
                _playerMovement._extendedJump = true;
                break;
            case STEP.E:
                _playerManager._canInteractSpecial = true;
                break;
            case STEP.P:
                //give the player more attack power
                break;
        }
    }
}
public enum STEP
{
    S,
    T,
    E,
    P
}
