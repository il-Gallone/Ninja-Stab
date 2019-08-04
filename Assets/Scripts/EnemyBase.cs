using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public bool awakened = false;
    public Rigidbody2D rigid2D;
    public float bolasTime = 0;
    public GameObject player;
    public float rotationSpeed = 90f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip deathClip;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (bolasTime > 0)
        {
            bolasTime -= Time.deltaTime;
            if (bolasTime <= 0)
            {
                rotationSpeed *= 2;
            }
        }
    }

    public void PlayDeathClip()
    {
        audioSource.clip = deathClip;
        audioSource.Play();
    }

    public virtual void Backstab()
    {
        PlayDeathClip();
        Destroy(gameObject);
    }

    public virtual void BolasAttack()
    {
        rotationSpeed /= 2;
        awakened = true;
        bolasTime = 5f;
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


    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Smoke")
        {
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Smoke")
        {
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }
}
