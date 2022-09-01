using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject itemPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (GAMESTATE.highlightedItem) {
                // If an item is highlighted, activate it
                Destroy(GAMESTATE.highlightedItem.transform.parent.gameObject);
            } else {
                // If an item isn't highlighted, activate the current item        
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
