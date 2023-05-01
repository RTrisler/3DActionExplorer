using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBars : MonoBehaviour
{
    public static event Action<float, float, Transform> OnEnterBossRoom;

    [SerializeField]
    private Transform _cameraPosition;

    [SerializeField]
    private float _lookAngle;

    [SerializeField]
    private float _pivotAngle;

    private Animator _barsAnim;
    private PlayerManager _player;

    private bool _hasOpened = false;

    [SerializeField]
    private BoxCollider _enterTrigger;
    void Start()
    {
        _barsAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            if (!_hasOpened)
            {
                _hasOpened = true;
                _player = player;
                _barsAnim.SetBool("bossAlive", true);
                OnEnterBossRoom?.Invoke(_lookAngle, _pivotAngle, _cameraPosition);
                _player.cameraManager.HandleAllCameraMovement();
                _player.isInteractingWithNPC = true;
            }
        }
    }

    private void revertCamera()
    {
        OnEnterBossRoom?.Invoke(_lookAngle, _pivotAngle, _player.transform);
        _player.isInteractingWithNPC = false;
        _enterTrigger.enabled = false;
    }
}
