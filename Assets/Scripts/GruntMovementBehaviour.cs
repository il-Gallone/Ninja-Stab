using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntMovementBehaviour : EnemyBase
{
    public float speed = 0.2f;
    

    // Update is called once per frame
    void Update()
    {
        if (awakened)
        {
            if (bolasTime > 0)
            {
                bolasTime -= Time.deltaTime;
            }
            else
            {
                Vector2 targetDirection = rigid2D.position - (Vector2)player.transform.position;
                if (player.GetComponent<PlayerController>().dashCharges > 1)
                {
                    rigid2D.velocity = targetDirection * speed;
                }
                else
                {
                    rigid2D.velocity = -targetDirection * speed;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rigid2D.velocity = new Vector2(0, 0);
        rigid2D.angularVelocity = 0;
        if (collision.gameObject == player)
        {
            if (!player.GetComponent<PlayerController>().dash)
            {
                Vector2 bounceDir = player.transform.position - transform.position;
                bounceDir.Normalize();
                player.GetComponent<PlayerController>().Damage();
                player.GetComponent<PlayerController>().BounceOffEnemy(bounceDir);
            }
        }
    }
}
