using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField]
    private Transform _startingPosition;
    public void movePlayer(PlayerManager player)
    {
        player.playerLocomotion.playerRigidBody.velocity = new Vector3(0, 0, 0);
        player.transform.position = _startingPosition.position;
    }
}
