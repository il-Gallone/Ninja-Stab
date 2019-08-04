using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarThrowerBehaviour : EnemyBase
{
    public float speed = 1.8f;
    public GameObject starPrefab;
    float throwTime = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (awakened)
        {
            if(bolasTime <= 0)
            {
                Vector2 targetDirection = rigid2D.position - (Vector2)player.transform.position;
                targetDirection.Normalize();
                if (Vector3.Distance(player.GetComponent<PlayerController>().transform.position, transform.position) <= 3)
                {
                    rigid2D.velocity = targetDirection * speed;
                }
                else
                {
                    rigid2D.velocity = new Vector2(0,0);
                    if (throwTime <= 0)
                    {
                        GameObject star = GameObject.Instantiate(starPrefab, transform.position, transform.rotation);
                        star.GetComponent<StarProjectile>().direction = -targetDirection;
                        Destroy(star, 1f);
                        throwTime = 3;
                    }
                    throwTime -= Time.deltaTime;
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
