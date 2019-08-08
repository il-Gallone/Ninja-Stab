using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemManager : MonoBehaviour
{
    public string itemID;
    GameObject player;

    private void Start()
    {
        //Find the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player collides with the item, if the player has no item, pick up the item and destroy object.
        if(collision.gameObject == player)
        {
            if(player.GetComponent<PlayerController>().item == "none")
            {
                player.GetComponent<PlayerController>().item = itemID;
                Destroy(gameObject);
            }
        }
    }
}
