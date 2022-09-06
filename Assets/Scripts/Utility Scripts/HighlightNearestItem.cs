using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightNearestItem : MonoBehaviour
{
    float searchRadius = 2f;
    public LayerMask highlightObjectLayer;

    void Update() {
        // Get the closest collider object
        GameObject closestObject = null;
        float minDistance = Mathf.Infinity;
        float currentDistance;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, highlightObjectLayer);

        // Get the clostest object hit
        foreach (Collider2D hitCollider in hitColliders) {
            currentDistance = (transform.position - hitCollider.gameObject.transform.position).sqrMagnitude;
            if (currentDistance < minDistance) {
                closestObject = hitCollider.gameObject;
                minDistance = currentDistance; 
            }
        }

        // Always start with dehighlighting previous objects
        if (GAMESTATE.highlightedItem) {
            GAMESTATE.highlightedItem.SendMessage("DeactivateHighlight");
        }

        // Highlight the new closest object
        if (closestObject) {
            closestObject.SendMessage("ActivateHighlight");
            GAMESTATE.highlightedItem = closestObject;
        } else {
            GAMESTATE.highlightedItem = null;
        }
    }

    // Draw a circle radius to see the search circle size
    void OnDrawGizmos() {
        // Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, searchRadius);
    }

}
