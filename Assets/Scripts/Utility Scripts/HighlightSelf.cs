using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelf : MonoBehaviour
{
    public Material baseMaterial;
    public Material outlineMaterial;

    void DeactivateHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().material = baseMaterial;
    }

    void ActivateHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().material = outlineMaterial;
    }
}
