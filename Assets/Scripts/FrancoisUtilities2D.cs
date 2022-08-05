using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Francois.Utilities2D {
    public static class FrancoisUtilities2D {

        // Notes on all the logic follow in this 2D scipt (which would be very differen to 3D scripts)
        // transform.forward is always into the camera
        // transform.right is always to the screen-right of a upright normal standing object
        // transform.up is always to the screen-up of a upright normal standing object
        // A 0 degree angle for an object would & should always point to the right
        // Objects always rotate only on the Z axis (never on X or Y) for spinning around

        // GET FUNCTIONS

        public static float GetAngleFromVector3(Vector3 direction) {
            // This will take in a vector3 and return an angle value
            direction = direction.normalized;
            float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            return n;
        }

        public static float GetAngleFromOneObjectToAnother(GameObject fromObject, GameObject toObject) {
            // This function returns the angle from one object towards another object
            float xChange = toObject.transform.position.x - fromObject.transform.position.x;
            float yChange = toObject.transform.position.y - fromObject.transform.position.y;
            float angleTowardsToObject = Mathf.Atan2(yChange, xChange) * Mathf.Rad2Deg;
            return angleTowardsToObject;
        }

        public static List<GameObject> GetAllParentsUpwards(GameObject fromObject) {
            // This function will return all the parents from this game object going upwards

            List<GameObject> parents = new List<GameObject>();
            GameObject currentObject = fromObject;

            while (currentObject.transform.parent) {
                parents.Add(currentObject);
                currentObject = currentObject.transform.parent.gameObject;
            }

            return parents;
        }

        // VOID FUNCTIONS

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
            // This rotates the fromObject to face the toObject
            // https://answers.unity.com/questions/1350050/how-do-i-rotate-a-2d-object-to-face-another-object.html

            // Here is another alternative if you only need to move a certain amount towards the object (this not the code being used in this function)
            // This function rotates the fromObject to face the toObject
            // https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html

            float angleTowardsObject = GetAngleFromOneObjectToAnother(fromObject, toObject);
            fromObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleTowardsObject));
        }

        public static void DebugDrawTowardsTransformDirection(GameObject fromObject, string transformDirection) {
            // This function will draw a debug line from the give object towards the angle supplied

            // Get the point we need to draw our line towards
            Vector3 towardsPoint;
            string transformDirectionLower = transformDirection.ToLower();
            switch(transformDirectionLower) {
                case "right":
                    towardsPoint = fromObject.transform.position + ( fromObject.transform.right * 5f );
                    break;
                case "forward":
                    towardsPoint = fromObject.transform.position + ( fromObject.transform.forward * 5f );
                    break;
                case "up":
                    towardsPoint = fromObject.transform.position + ( fromObject.transform.up * 5f );
                    break;
                default:
                    throw new Exception("DebugDrawTowardsTransformDirection() transformDirection can onlt be one of [\"up\", \"forward\", \"right\"]");
            }

            Debug.DrawLine(fromObject.transform.position, towardsPoint, Color.white);
        }

        public static void DebugDrawTowardsAngle(GameObject fromObject, float angle) {
            // This function will draw a debug line from the give object towards the angle supplied
            var line = fromObject.transform.position + ( fromObject.transform.right * 5f );
            var rotatedLine = Quaternion.AngleAxis( angle, fromObject.transform.up ) * line;
            Debug.DrawLine(fromObject.transform.position, rotatedLine, Color.white);
        }

    }    
}
    
