using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public bool awakened = false;
    public float speed;
    public Rigidbody2D rigid2D;
    public float bolasTime = 0;
    public GameObject player;
    public float rotationSpeed = 90f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip deathClip;

    // Start is called before the first frame update
    void Awake()
    {
        //Find the Player
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //Bolas Countdown
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
        //Start Bolas Cooldown and Slow Rotation
        rotationSpeed /= 2;
        awakened = true;
        bolasTime = 5f;
    }

    public void Alert()
    {
        //Allows some code to function on enemies
        awakened = true;
    }

    public float AngleFinder()
    {
        //Use Targetdirection to rotate angle
        Vector2 targetDirection = FindDirection(); //Normallize Direction
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) / Mathf.PI * 180 + 90; //Find the angle of the direction
        return targetAngle;
    }

    public void Flee()
    {
        Vector2 targetDirection = FindDirection();
        rigid2D.velocity = targetDirection * speed;
    }

    public void Chase()
    {
        Vector2 targetDirection = FindDirection();
        rigid2D.velocity = -targetDirection * speed;
    }

    //Get Target Direction
    public Vector2 FindDirection()
    {
        Vector2 targetDirection = rigid2D.position - (Vector2)player.transform.position;
        targetDirection.Normalize();
        return targetDirection;
    }
    

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        //If the enemy is in a smoke cloud turn off the front barrier,
        if (collision.tag == "Smoke")
        {
            gameObject.GetComponentInChildren<PolygonCollider2D>().isTrigger = true;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        //Turn back on the front barrier if smoke disappears or unit moves out of smoke
        if (collision.tag == "Smoke")
        {
            gameObject.GetComponentInChildren<PolygonCollider2D>().isTrigger = false;
        }
    }
}
