using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBuildingCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // We need to do this so that it ignores the wall and zombie colliders
        if (collider.name == "Player") {
            OutsideLightsController.ToggleLights();
        }
    }
}
