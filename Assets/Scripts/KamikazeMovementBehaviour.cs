using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeMovementBehaviour : EnemyBase
{
    public float speed = 2f;
    public bool fuseLit = false;
    public float fuseTime = 0;
    public GameObject explosionPrefab;

    // Update is called once per frame
    void Update()
    {
        if (awakened)
        {

            if (bolasTime <= 0 && fuseTime <= 4)
            {
                Vector2 targetDirection = rigid2D.position - (Vector2)player.transform.position;
                targetDirection.Normalize();
                rigid2D.velocity = -targetDirection * speed;
            }
            if (fuseLit)
            {
                fuseTime -= Time.deltaTime;
                if(fuseTime <= 0)
                {
                    Detonation();
                }
            }
        }
    }

    public override void Backstab()
    {
        if (!fuseLit)
        {
            fuseLit = true;
            fuseTime = 5;
            speed = 2.4f;
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(fuseLit && fuseTime <= 4)
        {
            if (collision.gameObject == player)
            {
                Detonation();
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if(!fuseLit && collision.tag == "Smoke")
        {
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }

    void Detonation()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
