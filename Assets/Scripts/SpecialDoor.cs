using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDoor : MonoBehaviour, ISpecialInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
