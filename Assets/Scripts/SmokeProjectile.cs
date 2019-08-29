using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeProjectile : MonoBehaviour
{
    public Vector2 direction;
    public GameObject smokePrefab;
    
    private void Update()
    {
        //Move in direction given by player
        transform.position += (Vector3)direction * 8 * Time.deltaTime;
    }

    private void OnDestroy()
    {
        //Create smoke cloud when destroyed
        Instantiate(smokePrefab, transform.position, transform.rotation);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //Automatically destroy when collding with non-player object
        if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
    
