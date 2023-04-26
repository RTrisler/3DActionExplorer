using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STEPManager : MonoBehaviour
{
    public float _charge = 0;
    private StepState _currentStepState = StepState.Uncharged;
    public void HandleStepState(MovementState playerMovementState, PlayerLocomotion playerLocomotion)
    {
        switch(playerMovementState)
        {
            case MovementState.Uncharging://handles STEP uncharge if the player is not moving
                if (_charge <= 0)
                {
                    
                }
                else
                {
                    _charge -= Time.deltaTime;
                }
                //Debug.Log(_charge);
                //Debug.Log(_currentStepState);
                switch (_currentStepState)
                {
                    case StepState.Uncharged:
                        break;
                    case StepState.S:
                        if (_charge <= 0)
                        {
                            _currentStepState = StepState.Uncharged;
                        }
                        break;
                    case StepState.T:
                        if (_charge <= 5)
                        {
                            _currentStepState = StepState.S;
                            playerLocomotion.SlowDownMovement();
                        }
                        break;
                    case StepState.E:
                        if (_charge <= 10)
                        {
                            _currentStepState = StepState.T;
                        }
                        break;
                    case StepState.P:
                        if (_charge <= 15)
                        {
                            _currentStepState = StepState.E;
                        }
                        break;
                    case StepState.FullyCharged:
                        if (_charge <= 20)
                        {
                            _currentStepState = StepState.P;
                        }
                        break;
                }
                break;
            case MovementState.Charging: //handles STEP charge if the player is moving
                if (_charge >= 20)
                {

                }
                else
                {
                    _charge += Time.deltaTime;
                }
                _charge += Time.deltaTime;
                //Debug.Log(_charge);
                //Debug.Log(_currentStepState);
                switch (_currentStepState)
                {
                    case StepState.Uncharged:
                        if(_charge > 0)
                        {
                            _currentStepState = StepState.S;
                        }
                        break;
                    case StepState.S:
                        if(_charge >= 5)
                        {
                            _currentStepState = StepState.T;
                            playerLocomotion.SpeedUpMovement();
                        }
                        break;
                    case StepState.T:
                        if(_charge >= 10)
                        {
                            _currentStepState = StepState.E;
                        }
                        break;
                    case StepState.E:
                        if(_charge >= 15)
                        {
                            _currentStepState = StepState.P;
                        }
                        break;
                    case StepState.P:
                        if(_charge >= 20)
                        {
                            _currentStepState = StepState.FullyCharged;
                        }
                        break;
                    case StepState.FullyCharged:
                        break;
                }
                break;
        }
    }
}
public enum StepState
{
    Uncharged,
    S,
    T,
    E,
    P,
    FullyCharged
}
public enum MovementState
{
    Uncharging,
    Charging,
}
