using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTransform : MonoBehaviour
{
    [SerializeField] GameObject syncToObject;

    void Update()
    {
        // Each frame, sync this object's values to the syncToObject
        this.transform.position = new Vector3(syncToObject.transform.position.x, syncToObject.transform.position.y, this.transform.position.z);
    }
}
