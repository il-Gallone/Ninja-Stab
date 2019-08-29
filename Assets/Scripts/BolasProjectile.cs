using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasProjectile : MonoBehaviour
{
    public Vector2 direction;
    Rigidbody2D rigid2D;

    void Start()
    {
        //Finding the rigidbody
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Moving and rotating the bolas
        rigid2D.position += direction * 6 * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, 0, 360 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")//On Collision with Enemy
        {
            collision.gameObject.SendMessageUpwards("BolasAttack"); //Trigger the bolas function
            Destroy(gameObject);
        }
        else if(collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
