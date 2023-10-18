using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : GenericSingletonClass<AudioManager>
{
    AudioSource audioSource;
    public AudioClip placeSound;
    public AudioClip gatherSound;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayPlaceSound()
    {
        Debug.Log("Play sound:");
        audioSource.PlayOneShot(placeSound);
    }
    public void PlayGatherSound()
    {
        audioSource.PlayOneShot(gatherSound);
    }
}
