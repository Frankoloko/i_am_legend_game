// HOW TO USE THIS SCRIPT
    // Just create any type of game object and throw this script into it (it will be the thing that moves)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 0.01f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        gameObject.transform.position = new Vector3 (transform.position.x + (h * speed), transform.position.y + (v * speed), transform.position.z);
    }
}
