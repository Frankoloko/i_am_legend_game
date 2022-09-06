using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RND : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
    }
}
