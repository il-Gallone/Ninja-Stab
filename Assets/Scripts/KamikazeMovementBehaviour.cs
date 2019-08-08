using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeMovementBehaviour : EnemyBase
{
    public bool fuseLit = false;
    public float fuseTime = 0;
    public GameObject explosionPrefab;

    private void Start()
    {
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (awakened)
        {
            //If not under effect of bolas or not lighting fuse chase player
            if (bolasTime <= 0 && fuseTime <= 4)
            {
                Chase();
            }
            if (fuseLit)//Start counting down fuse
            {
                fuseTime -= Time.deltaTime;
                if(fuseTime <= 0)
                {
                    //Explode on time out
                    Detonation();
                }
            }
        }
    }

    public override void Backstab()
    {
        //Instead of dying, turn off front barrier and light the fuse
        if (!fuseLit)
        {
            fuseLit = true;
            fuseTime = 5;
            speed = 3.6f;
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Automatically explode when colliding with the player with a lit fuse
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
        //Avoiding the reenable of the front collider out of smoke if the fuse is lit
        if(!fuseLit && collision.tag == "Smoke")
        {
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }

    void Detonation()
    {
        //Spawn the explosion and destroy
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
