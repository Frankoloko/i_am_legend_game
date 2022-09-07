using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelf : MonoBehaviour
{
    public Material baseMaterial;
    public Material outlineMaterial;
    public GameObject spriteObject;

    void DeactivateHighlight()
    {
        spriteObject.GetComponent<SpriteRenderer>().material = baseMaterial;
    }

    void ActivateHighlight()
    {
        spriteObject.GetComponent<SpriteRenderer>().material = outlineMaterial;
    }
}
