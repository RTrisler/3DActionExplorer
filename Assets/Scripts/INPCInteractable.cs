using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform _cameraPosition;

    [SerializeField]
    private CameraManager _cameraManager;

    public void Interact()
    {

    }
}
