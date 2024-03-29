﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //Expand while scale is smaller than 1.5
        if (transform.localScale.x < 1.5f && transform.localScale.y < 1.5f)
        {
            transform.localScale += new Vector3(1.5f, 1.5f, 0) * Time.deltaTime;
        }
        else//Once scale is 3 destroy
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //Hurt the player when inside the explosion
        if (collision.tag == "Player")
        {
            collision.gameObject.SendMessage("Damage");
        }
    }
}
