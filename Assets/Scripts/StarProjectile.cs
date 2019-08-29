using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarProjectile : MonoBehaviour
{
    public Vector2 direction;
    Rigidbody2D rigid2D;

    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Fly in thrown direction, also rotate star
        rigid2D.position += direction * 12f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, 0, 360 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If collides with a player deal damage and destroy the star.
        if (collision.tag == "Player")
        {
            collision.gameObject.SendMessage("Damage");
            Destroy(gameObject);
        }
        else if(collision.tag != "Enemy")
        {
            //Destroy the star if colliding with a non-player collider
            Destroy(gameObject);
        }
    }
}
