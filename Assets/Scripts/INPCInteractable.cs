using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform _cameraPosition;

    [SerializeField]
    private CameraManager _cameraManager;

    [SerializeField]
    private string _interactUi;

    public void Interact()
    {

    }

    public string SendInteractUi()
    {
        return _interactUi;
    }
}
