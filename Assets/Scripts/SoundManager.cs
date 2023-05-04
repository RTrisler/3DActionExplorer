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
    }
}
