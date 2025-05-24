using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNextAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip clip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundNow()
    {
        _audioSource.PlayOneShot(clip,0.67f);
    }
}
