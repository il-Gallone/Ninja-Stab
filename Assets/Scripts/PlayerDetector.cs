using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If player is in alert range, start code on enemy and destroy the alert checker.
        if(collision.tag == "Player")
        {
            SendMessageUpwards("Alert");
            Destroy(gameObject);
        }
    }
}
