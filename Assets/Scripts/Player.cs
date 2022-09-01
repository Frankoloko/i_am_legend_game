using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Player : MonoBehaviour
{
    public GameObject itemPrefab;

    float outsideLightsChangeValue = 0.01f;
    float outsideLightsStartingValue = 1f;
    bool outsideLightsTurnedOn = true;

    void Start() {
        // Here we just force all lights on, for some reason stopping and starting the game doesn't reset the light intensities
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "OutsideLight");
        foreach (GameObject item in objects)
        {
            item.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = outsideLightsStartingValue;
        }
    }

    void FixedUpdate()
    {
        // if (outsideLightsTurnedOn) {
        //     TurnOffOutsideLights();
        // } else {
        //     TurnOnOutsideLights();
        // }
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (GAMESTATE.highlightedItem) {
                // If an item is highlighted, activate it
                Destroy(GAMESTATE.highlightedItem.transform.parent.gameObject);
            } else {
                // If an item isn't highlighted, activate the current item        
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    void TurnOffOutsideLights()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "OutsideLight");
        foreach (GameObject item in objects)
        {
            item.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity -= outsideLightsChangeValue;
            if (item.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity <= 0f) {
                outsideLightsTurnedOn = false;
            }
        }
    }

    void TurnOnOutsideLights()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "OutsideLight");
        foreach (GameObject item in objects)
        {
            item.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity += outsideLightsChangeValue;
            if (item.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity >= 1f) {
                outsideLightsTurnedOn = true;
            }
        }
    }
}
