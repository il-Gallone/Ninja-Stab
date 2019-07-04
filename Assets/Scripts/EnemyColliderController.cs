using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rigid2D;
    bool blocked = false;
    float smokeTime = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rigid2D.velocity = new Vector2(0, 0);
        rigid2D.angularVelocity = 0;
        if (collision.gameObject == player)
        {
            if (player.GetComponent<PlayerController>().dash)
            {
                Vector2 bounceDir = player.transform.position - transform.position;
                bounceDir.Normalize();
                player.GetComponent<PlayerController>().BounceOffEnemy(bounceDir);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if (player.GetComponent<PlayerController>().dash)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Smoke")
        {
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Smoke")
        {
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }
}
