using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField]
    private AudioSource _soundEffect;

    [SerializeField]
    private AudioSource _gameMusic;

    [SerializeField]
    private float _fadeSpeed;

    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        
    }
    private void Start()
    {
        _gameMusic.Play();
    }

    public void playSoundQuick(AudioClip audioToPlay)
    {
        _soundEffect.clip = audioToPlay;
        _soundEffect.volume = 1;
        _soundEffect.Play();
    }

    public void playSoundQuick(AudioClip audioToPlay, float clipVolume)
    {
        _soundEffect.clip = audioToPlay;
        _soundEffect.volume = clipVolume;
        _soundEffect.Play();
    }

    public void playSoundFadeIn(AudioClip fadeAudio)
    {
        _soundEffect.clip = fadeAudio;
        _soundEffect.volume = 0;
        _soundEffect.Play();
        StartCoroutine(fadeSound());
    }

    public void fadeSoundOut()
    {
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeSound()
    {
        if(_soundEffect.volume == .5)
        {
            StopAllCoroutines();
        }
        else
        {
            Debug.Log(_soundEffect.volume);
            _soundEffect.volume += _fadeSpeed;
            yield return new WaitForSeconds(.2f);
            StartCoroutine(fadeSound());
        }
    }

    IEnumerator fadeOut()
    {
        if (_soundEffect.volume == 0)
        {
            StopAllCoroutines();
        }
        else
        {
            _soundEffect.volume -= _fadeSpeed;
            yield return new WaitForSeconds(.2f);
            StartCoroutine(fadeSound());
        }
    }
}
