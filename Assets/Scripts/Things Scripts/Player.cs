using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject itemPrefab;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy")) {
            Time.timeScale = 0;
            Debug.Log("DEAD!!! GAME OVER");
        }
    }
}
