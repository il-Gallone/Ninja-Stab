using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarThrowerBehavior : MonoBehaviour
{
    bool awakened = false;
    public Rigidbody2D rigid2D;
    public float speed = 0.6f;
    float bolasTime = 0;
    GameObject player;
    public GameObject starPrefab;
    float throwTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
                        Destroy(star, 3.6f);
                        throwTime = 3;
                    }
                    throwTime -= Time.deltaTime;
                }
            }
        }
    }

    public void Alert()
    {
        awakened = true;
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


    public void BolasAttack()
    {
        awakened = true;
        bolasTime = 5f;
    }
}
