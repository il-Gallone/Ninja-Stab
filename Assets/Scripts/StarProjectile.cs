﻿using System.Collections;
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
        rigid2D.position += direction * 1.5f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, 0, 360 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.SendMessage("Damage");
            Destroy(gameObject);
        }
    }
}