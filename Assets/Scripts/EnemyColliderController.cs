using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rigid2D;

    private void Start()
    {
        //Find the player
        player = GameObject.FindGameObjectWithTag("Player");
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Reset force to fix collision physics.
        rigid2D.velocity = new Vector2(0, 0);
        rigid2D.angularVelocity = 0;
        //check if collision is the player
        if (collision.gameObject == player)
        {
            //check if the player is dashing
            if (player.GetComponent<PlayerController>().dash)
            {
                //Knockback the player without hurting it
                Vector2 bounceDir = player.transform.position - transform.position;
                bounceDir.Normalize();
                player.GetComponent<PlayerController>().BounceOffEnemy(bounceDir);
            }
            else //If player is not dashing
            {
                //Knockback the player and hurt it
                Vector2 bounceDir = player.transform.position - transform.position;
                bounceDir.Normalize();
                player.GetComponent<PlayerController>().Damage();
                player.GetComponent<PlayerController>().BounceOffEnemy(bounceDir);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the player hits the trigger
        if (collision.gameObject == player)
        {
            //Check if the player is dashing
            if (player.GetComponent<PlayerController>().dash)
            {
                //Perform the hit function
                gameObject.SendMessageUpwards("Backstab");
            }
        }
    }
}
