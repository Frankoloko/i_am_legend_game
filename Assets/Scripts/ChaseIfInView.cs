using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Francois.Utilities2D;

public class ChaseIfInView : MonoBehaviour
{

    [SerializeField] GameObject fieldOfViewObject;
    FieldOfView fieldOfViewScript;

    public GameObject chaseObject;

    private float viewDistance;

    void Start()
    {
        fieldOfViewScript = fieldOfViewObject.GetComponent<FieldOfView>();
    }

    void Update()
    {
        // FrancoisUtilities2D.ExamplePrint("test");
        bool withView = fieldOfViewScript.CheckIfWithinView(chaseObject);
        if (withView) {
            FrancoisUtilities2D.RotateTowardsObject(this.gameObject, chaseObject, 0.1f);
            FrancoisUtilities2D.MoveTowardsObject(this.gameObject, chaseObject, 0.005f);
        }
    }
}
