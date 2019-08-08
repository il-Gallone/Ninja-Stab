using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarThrowerBehaviour : EnemyBase
{
    public GameObject starPrefab;
    float throwTime = 0;

    private void Start()
    {
        speed = 2.7f;
    }

    // Update is called once per frame
    void Update()
    {
        //If alerted
        if (awakened)
        {
            if(bolasTime <= 0)
            {
                //If player is in personal space run away
                if (Vector3.Distance(player.GetComponent<PlayerController>().transform.position, transform.position) <= 3)
                {
                    Flee();
                }
                else
                {
                    //If player is not in personal space stay still and throw stars on a timer
                    rigid2D.velocity = new Vector2(0,0);
                    if (throwTime <= 0)
                    {
                        GameObject star = GameObject.Instantiate(starPrefab, transform.position, transform.rotation);
                        star.GetComponent<StarProjectile>().direction = -FindDirection();
                        Destroy(star, 1f);
                        throwTime = 3;
                    }
                    throwTime -= Time.deltaTime;
                }
            }
        }
    }
}
