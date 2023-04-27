using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
