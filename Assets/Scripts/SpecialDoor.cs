using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDoor : MonoBehaviour, ISpecialInteractable
{
    [SerializeField]
    private Animator _myAnim;

    [SerializeField]
    private string _interactUi;

    [SerializeField]
    private AudioClip _doorAudio;
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

    public string SendInteractUi()
    {
        return _interactUi;
    }

    private void playAudio()
    {
        SoundManager.Instance.playSoundQuick(_doorAudio);
    }
}
