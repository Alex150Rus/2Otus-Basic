using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    [SerializeField] 
    private DataSound[] dataSounds;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(string nameClip)
    {
        AudioClip clip = GetAudioClip(nameClip);
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    private AudioClip GetAudioClip(string nameClip)
    {
        AudioClip clip = null;
        foreach (var sound in dataSounds)
        {
            if (sound.name == nameClip)
            {
                clip = sound.audioClip;
                break;
            }
        }
        return clip;
    }

    [Serializable]
    private class DataSound
    {
        public string name;
        public AudioClip audioClip;
    }
}
