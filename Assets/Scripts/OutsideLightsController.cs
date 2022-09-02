using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Francois.Utilities2D;

public class OutsideLightsController : MonoBehaviour
{
    // Lights related variables
    IEnumerable<GameObject> outsideLights;
    float lightsChangeValue = 0.07f;
    float lightsStartingValue = 2f;
    static bool lightsOn = true;
    static bool lightsBusyChanging = false;

    // House View Covers related values
    IEnumerable<GameObject> houseViewCoversRoofs;
    float houseViewCoverChangeValue = 0.1f;

    void Start() {
        outsideLights = FrancoisUtilities2D.GetObjectsWithName("OutsideLight");
        houseViewCoversRoofs = FrancoisUtilities2D.GetObjectsWithName("HouseViewCoverRoof");

        // Use these to trigger everything to turn on
        lightsBusyChanging = true;
        lightsOn = false;
    }

    void FixedUpdate()
    {
        // We only change the lights if they are in their changing state
        if (lightsBusyChanging) {
            // Here we call the functions that will turn the lights on or off by a little amount
            if (lightsOn) {
                TurnOffOutsideLights();
                TurnOffHouseViewCover();
            } else {
                TurnOnOutsideLights();
                TurnOnHouseViewCover();
            }
        }
    }

    public static void ToggleLights()
    {
        // Toggle whether the lights are on or off
        if (lightsOn) {
            lightsOn = false;
        } else {
            lightsOn = true;
        }

        lightsBusyChanging = true;
    }

    void TurnOffOutsideLights()
    {
        // This function will turn OFF all the "OutsideLight" lights
        foreach (GameObject light in outsideLights)
        {
            light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity -= lightsChangeValue;
            if (light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity <= 0f) {
                lightsOn = false;
                lightsBusyChanging = false;
            }
        }
    }

    void TurnOnOutsideLights()
    {
        // This function will turn ON all the "OutsideLight" lights
        foreach (GameObject light in outsideLights)
        {
            light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity += lightsChangeValue;
            if (light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity >= lightsStartingValue) {
                lightsOn = true;
                lightsBusyChanging = false;
            }
        }
    }

    void TurnOffHouseViewCover()
    {
        // This will gradually change the opacity of the house view covers to 0
        foreach (GameObject houseViewCoverRoof in houseViewCoversRoofs)
        {
            Color color = houseViewCoverRoof.GetComponent<SpriteRenderer>().color;
            color.a -= houseViewCoverChangeValue;
            houseViewCoverRoof.GetComponent<SpriteRenderer>().color = color;
        }
    }

    void TurnOnHouseViewCover()
    {
        // This will gradually change the opacity of the house view covers to solid
        foreach (GameObject houseViewCoverRoof in houseViewCoversRoofs)
        {
            Color color = houseViewCoverRoof.GetComponent<SpriteRenderer>().color;
            color.a += houseViewCoverChangeValue;
            houseViewCoverRoof.GetComponent<SpriteRenderer>().color = color;
        }
    }

}
