using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockArm : MonoBehaviour
{
    public float levelDurationMin;
    
    float rotationIncrement;
    float currentTotal;

    void Start() {
        // Get the increments at which the arm will rotate
        rotationIncrement = 360f / (levelDurationMin * 60 * 50);
        currentTotal = 0f;
    }

    void FixedUpdate() {
        // Rotate the arm
        transform.Rotate(0.0f, 0.0f, rotationIncrement * -1, Space.Self);
        currentTotal += rotationIncrement;

        // Check when the arm has rotated completely
        if (currentTotal > 360f) {
            Debug.Log("Time over!");
        }
    }
}
