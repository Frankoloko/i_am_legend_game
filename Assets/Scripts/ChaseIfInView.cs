using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Francois.Utilities;

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
        // FrancoisUtilities.ExamplePrint("test");
        bool withView = fieldOfViewScript.CheckIfWithinView(chaseObject);
        if (withView) {
            FrancoisUtilities.RotateTowardsObject(this.gameObject, chaseObject, 1f);
            FrancoisUtilities.MoveTowardsObject(this.gameObject, chaseObject, 0.01f);
        }
    }
}
