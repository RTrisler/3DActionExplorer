using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{
    [SerializeField]
    private float _pushForce;

    [SerializeField]
    private AudioClip _windAudio;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<PlayerLocomotion>(out PlayerLocomotion locomotion))
        {
            locomotion.playerRigidBody.AddForce(this.transform.forward * _pushForce);
        }
        if(other.TryGetComponent<PuzzleBall>(out PuzzleBall ball))
        {
            ball._ballRigidBody.AddForce(this.transform.forward * 30);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            Debug.Log("Enterd trigger");
            SoundManager.Instance.playSoundFadeIn(_windAudio);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            Debug.Log("Exit trigger");
            SoundManager.Instance.fadeSoundOut();
        }
    }

}
