using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSTEPManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
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
                //increase speed and allow sprinting
                break;
            case STEP.T:
                //allow player to do super jump
                break;
            case STEP.E:
                //allow player to enter speical rooms
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
