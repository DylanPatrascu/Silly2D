using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip song;
    public AudioSource audioSource;
    
    private void Awake()
    {
        audioSource.spatialBlend = 0.0f;
        audioSource.clip = song;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
    }
}