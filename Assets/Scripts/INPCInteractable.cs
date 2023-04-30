using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class INPCInteractable : MonoBehaviour, IInteractable
{
    public static event Action<float, float, Transform> OnNPCInteract;

    [SerializeField]
    private Transform _cameraPosition;

    [SerializeField]
    private float _lookAngle;

    [SerializeField]
    private float _pivotAngle;

    [SerializeField]
    private string _interactUi;

    private bool _isInteracting = false;

    public void InteractNPC(Transform playerTransform)
    {
        if (!_isInteracting)
        {
            OnNPCInteract?.Invoke(_lookAngle, _pivotAngle, _cameraPosition);
            _isInteracting = true;
        }
        else
        {
            OnNPCInteract?.Invoke(_lookAngle, _pivotAngle, playerTransform);
            _isInteracting = false;
        }
    }

    public string SendInteractUi()
    {
        return _interactUi;
    }

    public void Interact()
    {
        return;
    }
}
