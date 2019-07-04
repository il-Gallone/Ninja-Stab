using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemManager : MonoBehaviour
{
    public string itemID;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
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
