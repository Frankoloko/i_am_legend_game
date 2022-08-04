using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Francois.Utilities2D {
    public static class FrancoisUtilities2D {

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

            float angleTowardsObject = AngleFromOneObjectToAnother(fromObject, toObject);
            fromObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleTowardsObject));
        }

        public static float AngleFromOneObjectToAnother(GameObject fromObject, GameObject toObject) {
            // This function returns the angle from one object towards another object
            float xChange = toObject.transform.position.x - fromObject.transform.position.x;
            float yChange = toObject.transform.position.y - fromObject.transform.position.y;
            float angleTowardsToObject = Mathf.Atan2(yChange, xChange) * Mathf.Rad2Deg;
            return angleTowardsToObject;
        }

        public static void DebugDrawTowardsTransformDirection(GameObject fromObject, float angle, string transformDirection) {
            // This function will draw a debug line from the give object towards the angle supplied
            // Vector3 towardsPoint = fromObject.transform.position + ( fromObject.transform.forward * 5f );

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
        
    }    
}
    
