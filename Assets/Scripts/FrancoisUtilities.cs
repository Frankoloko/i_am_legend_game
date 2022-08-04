using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Francois.Utilities {
    public static class FrancoisUtilities {

        public static void ExamplePrint(string text) {
            // Keep this function just as an example, prints a string value
            Debug.Log(text);

            // Example of how to use in another script:
            // At the top of the script add: using Francois.Utilities;
            // Then just run the function anywhere you like: FrancoisUtilities.ExamplePrint("test");
        }

        public static void MoveTowardsObject(GameObject fromObject, GameObject toObject, float speed) {
            // This function moves the fromObject towards the toObject
            fromObject.transform.position = Vector3.MoveTowards(fromObject.transform.position, toObject.transform.position, speed);
        }

        public static void RotateTowardsObject(GameObject fromObject, GameObject toObject, float speed) {
            // This function rotates the fromObject to face the toObject
            // https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html

            // Determine which direction to rotate towards
            Vector3 targetDirection = toObject.transform.position - fromObject.transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(fromObject.transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            // Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            fromObject.transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }    
}
    
