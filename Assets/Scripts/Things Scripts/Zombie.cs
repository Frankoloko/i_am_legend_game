using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Francois.Utilities2D;

public class Zombie : MonoBehaviour
{
    public GameObject fieldOfViewObject;
    public string chaseObjectString;
    public AudioClip idleMp3;
    public AudioClip chaseMp3;
    public AudioClip deathMp3;

    FieldOfView fieldOfViewScript;
    float viewDistance;
    GameObject chaseObject;
    AudioSource audioSource;
    string state; // "alive" / "dead"
 
    void Start()
    {
        // Instead of letting the user assign a game object, we just let the user supply a string
        // and then we will find the game object. We do this because in prefab viewer, you can't reference
        // things outside of the prefab (so you can't drag the Player object into this field)
        chaseObject = GameObject.Find(chaseObjectString);

        fieldOfViewScript = fieldOfViewObject.GetComponent<FieldOfView>();
        audioSource = gameObject.GetComponent<AudioSource>();

        state = "alive";
    }

    void Update()
    {
        if (state == "dead") {
            return;
        }

        bool withView = fieldOfViewScript.CheckIfWithinView(chaseObject);
        if (withView) {
            FrancoisUtilities2D.RotateTowardsObject(this.gameObject, chaseObject, 0.1f);
            FrancoisUtilities2D.MoveTowardsObject(this.gameObject, chaseObject, 0.005f);

            // Set chase audio (if the chase audio is already set, we don't want to keep setting it on every update)
            if (audioSource.clip != chaseMp3) {
                audioSource.clip = chaseMp3;
                audioSource.volume = 0.6f;
                audioSource.time = 0f;
                audioSource.Play();
            }
        } else {
            // Set idle audio (if the chase audio is already set, we don't want to keep setting it on every update)
            if (audioSource.clip != idleMp3) {
                audioSource.clip = idleMp3;
                audioSource.volume = 0.3f;
                audioSource.time = 0f;
                audioSource.Play();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Contains("BearTrap")) {
            collider.gameObject.SendMessage("Close");
            Die(collider);
        }
    }

    void Die(Collider2D collider) {
        Debug.Log("Zombie died!");
        state = "dead";

        // Play the death sound
        audioSource.clip = deathMp3;
        audioSource.volume = 0.6f;
        audioSource.time = 0f;
        audioSource.loop = false;
        audioSource.Play();

        // Set the dead position right in the middle of the collided object
        this.transform.position = collider.gameObject.transform.position;
        
        // Set the dead rotation
        float rotationXValue = this.transform.rotation.x;
        float rotationYValue = this.transform.rotation.y;
        float rotationZValue = 115;
        this.transform.eulerAngles = new Vector3(rotationXValue, rotationYValue, rotationZValue);
    }
}
