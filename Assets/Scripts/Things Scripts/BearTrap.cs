using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    AudioSource audioSource;
    GameObject holdingObject;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        this.holdingObject = null;
    }

    void Close(GameObject caughtObject)
    {
        this.holdingObject = caughtObject;
        audioSource.timeSamples = 25000; // Set the audio to start fromthe middle since the close audio only snaps in the middle somewhere
        audioSource.Play();
    }

    void PlayerActivate()
    {
        GAMESTATE.holdingItems.Add(this.holdingObject);
        Object.Destroy(this.gameObject);
        Object.Destroy(this.holdingObject);
    }
}
