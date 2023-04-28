using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DoorHandler : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Animator _myAnim;

    void Start()
    {
        _myAnim = GetComponent<Animator>();
    }
    public void Interact()
    {
        _myAnim.SetBool("opening", true);
    }

    public void revertBool()
    {
        _myAnim.SetBool("opening", false);
    }
}
