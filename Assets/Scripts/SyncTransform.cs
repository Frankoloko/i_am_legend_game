using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTransform : MonoBehaviour
{
    [SerializeField] GameObject syncToObject;

    [SerializeField] bool positionX;
    [SerializeField] bool positionY;
    [SerializeField] bool positionZ;
    [SerializeField] bool rotationX;
    [SerializeField] bool rotationY;
    [SerializeField] bool rotationZ;
    [SerializeField] bool scaleX;
    [SerializeField] bool scaleY;
    [SerializeField] bool scaleZ;

    void Update()
    {
        // Each frame, sync this object's values to the syncToObject

        // result = (if statemtent) ? (if true) : (if false);

        float positionXValue = positionX ? syncToObject.transform.position.x : this.transform.position.x;
        float positionYValue = positionY ? syncToObject.transform.position.y : this.transform.position.y;
        float positionZValue = positionZ ? syncToObject.transform.position.z : this.transform.position.z;

        float scaleXValue = scaleX ? syncToObject.transform.localScale.x : this.transform.localScale.x;
        float scaleYValue = scaleY ? syncToObject.transform.localScale.y : this.transform.localScale.y;
        float scaleZValue = scaleZ ? syncToObject.transform.localScale.z : this.transform.localScale.z;

        float rotationXValue = rotationX ? syncToObject.transform.rotation.x : this.transform.rotation.x;
        float rotationYValue = rotationY ? syncToObject.transform.rotation.y : this.transform.rotation.y;
        float rotationZValue = rotationZ ? syncToObject.transform.rotation.z : this.transform.rotation.z;

        this.transform.position = new Vector3(positionXValue, positionYValue, positionZValue);
        this.transform.eulerAngles = new Vector3(rotationXValue, rotationYValue, rotationZValue);
        this.transform.localScale = new Vector3(scaleXValue, scaleYValue, scaleZValue);
    }
}
