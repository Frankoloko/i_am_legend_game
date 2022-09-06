using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Close()
    {
        audioSource.timeSamples = 25000; // Set the audio to start fromthe middle since the close audio only snaps in the middle somewhere
        audioSource.Play();
    }
}
