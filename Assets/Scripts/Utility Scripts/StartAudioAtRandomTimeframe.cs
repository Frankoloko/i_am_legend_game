using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioAtRandomTimeframe : MonoBehaviour
{
    
    void Start()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        int randomStartTime = Random.Range(0, audioSource.clip.samples - 1);
        audioSource.timeSamples = randomStartTime;
        audioSource.Play();
    }
}
