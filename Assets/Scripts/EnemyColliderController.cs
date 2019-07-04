using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderController : MonoBehaviour
{
    GameObject player;
    bool blocked = false;
    float smokeTime = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Smoke(float timeLeft)
    {
        smokeTime = timeLeft;
        StartCoroutine("SmokeDecay");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            if (player.GetComponent<PlayerController>().dash)
            {
                StartCoroutine("DamageBlocker");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine("DamageCheck");
        }
    }

    IEnumerator DamageBlocker()
    {
        blocked = true;
        Vector2 bounceDir = player.transform.position - transform.position;
        bounceDir.Normalize();
        player.GetComponent<PlayerController>().BounceOffEnemy(bounceDir);
        yield return new WaitForSeconds(0.1f);
        blocked = false;
    }

    IEnumerator DamageCheck()
    {
        yield return new WaitForSeconds(0.01f);
        if (player.GetComponent<PlayerController>().dash && !blocked)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SmokeDecay()
    {
        yield return new WaitForSeconds(smokeTime);
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
    }
}
