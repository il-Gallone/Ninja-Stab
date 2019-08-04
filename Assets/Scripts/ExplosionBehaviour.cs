using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 3 && transform.localScale.y < 3)
        {
            transform.localScale += new Vector3(1.5f, 1.5f, 0) * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.SendMessage("Damage");
        }
    }
}
