using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Francois.Utilities2D;

public class ChaseIfInView : MonoBehaviour
{

    [SerializeField] GameObject fieldOfViewObject;
    FieldOfView fieldOfViewScript;

    public string chaseObjectString;

    private float viewDistance;
    private GameObject chaseObject;

    void Start()
    {
        // Instead of letting the user assign a game object, we just let the user supply a string
        // and then we will find the game object. We do this because in prefab viewer, you can't reference
        // things outside of the prefab (so you can't drag the Player object into this field)
        chaseObject = GameObject.Find(chaseObjectString);

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
