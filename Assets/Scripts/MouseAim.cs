// HOW TO USE THIS SCRIPT
    // Just create any type of game object and throw this script into it (it will be the thing that rotates)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{

    // [SerializeField] private FieldOfView fieldOfView;

    private void Update() {
        // Point the object to the mouse position
        Vector3 mousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePostion.z = 0f;
        Vector3 aimDirection = (mousePostion - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        gameObject.transform.eulerAngles = new Vector3(0, 0, angle);

        // fieldOfView.SetAimDirection(aimDirection);
    }
}

