using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    public int health = 4;

    float speed = 4.5f;
    public float angle = 0;
    public bool dash = false;
    float dashTime = 0;
    public int dashCharges = 2;
    public float dashCooldown = 0;
    public float invulnTime = 0;
    bool bounce = false;
    float bounceTime = 0;
    Vector2 bounceDir;
    public string item = "none";
    public GameObject bolasPrefab;
    public GameObject smokePrefab;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    SpriteRenderer spriteRenderer;

    [Header("Audio")]
    public AudioSource walkSource;
    public AudioSource audioSource;
    public AudioClip dashClip;
    public AudioClip throwClip;
    public AudioClip hurtClip;
    public AudioClip deathClip;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal"); //Change Horizontal to X-axis when switching to PS controller
        float yAxis = Input.GetAxis("Vertical"); //Change Vertical to Y-axis when switching to PS controller
        //Get direction based on controller sticks and normalise it
        Vector2 direction = new Vector2(xAxis, yAxis);
        direction.Normalize();
        //If stick is moved change the angle
        if ((xAxis != 0 || yAxis != 0) && !dash)
            angle = Mathf.Atan2(yAxis, xAxis) / Mathf.PI * 180 - 90;
        //Change the sprite based on angle
        if (angle <= 45 && angle > -45)
        {
            spriteRenderer.sprite = up;
        }
        if (angle > 45 || angle <= -225)
        {
            spriteRenderer.sprite = left;
        }
        if (angle > -225 && angle <= -135)
        {
            spriteRenderer.sprite = down;
        }
        if (angle > -135 && angle <= -45)
        {
            spriteRenderer.sprite = right;
        }
        //move in the direction that the player pushes
        rigid2D.position += direction * speed * Time.deltaTime;
        if(direction != new Vector2(0,0))
        {
            if(!walkSource.isPlaying)
            {
                walkSource.Play();
            }
        } else
        {
            walkSource.Stop();
        }
        //Dash Input
        if (Input.GetKeyDown("joystick 1 button 0") && !bounce && dashCharges > 0)
        {
            audioSource.clip = dashClip;
            audioSource.Play();
            //Set dashing variables
            dash = true;
            dashTime = 0.18f;
            dashCharges--;
            //If Cooldown is not already active, activate the cooldown if under two dashes.
            if(dashCooldown <= 0 && dashCharges < 2)
                dashCooldown = 3f;
        }
        //Item Input
        if(Input.GetKeyDown("joystick 1 button 1") && item != "none")
        {
            //Finds which code to run for each item
            switch (item)
            {
                case "bolas":
                    //Reset the held item and spawn the bolas giving it a direction and destruction timer
                    item = "none";
                    GameObject bolas = GameObject.Instantiate(bolasPrefab, transform.position, transform.rotation);
                    bolas.GetComponent<BolasProjectile>().direction = new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180));
                    Destroy(bolas, 1.2f);
                    audioSource.clip = throwClip;
                    audioSource.Play();
                    break;
                case "smoke":
                    //Reset the held item and spawn the smoke giving it a direction and destruction timer
                    item = "none";
                    GameObject smoke = GameObject.Instantiate(smokePrefab, transform.position, transform.rotation);
                    smoke.GetComponent<SmokeProjectile>().direction = new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180));
                    Destroy(smoke, 0.5f);
                    audioSource.clip = throwClip;
                    audioSource.Play();
                    break;
                case "boost":
                    //Reset the held item and give the player three more dash charges
                    item = "none";
                    dashCharges += 3;
                    dashCooldown = 0;
                    break;
            }
        }
        //Dash Code
        if(dash)
        {
            //Dash length tracker and set dash speed
            dashTime -= Time.deltaTime;
            rigid2D.velocity = (new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180)) * 36);
            //If dash is out of time reset 
            if (dashTime < 0)
            {
                dash = false;
                rigid2D.velocity = new Vector2(0, 0);
            }
        }
        if(bounce)
        {
            //Similar code to dashing but with enemy knockback instead.
            bounceTime -= Time.deltaTime;
            rigid2D.velocity = bounceDir * 18;
            if (bounceTime < 0)
            {
                bounce = false;
                rigid2D.velocity = new Vector2(0, 0);
            }
        }
        //If the dash cooldown is active
        if(dashCooldown > 0)
        {
            //Countdown for Cooldown
            dashCooldown -= Time.deltaTime;
            if(dashCooldown <= 0)
            {
                //Once Cooldown is finished add a charge, and reset cooldown if still below 2 dashes
                dashCharges++;
                if(dashCharges < 2)
                {
                    dashCooldown += 3;
                }
            }
        }
        //If invulnerable from damage, reduce time of invulnerablity
        if(invulnTime >0)
        {
            invulnTime -= Time.deltaTime;
        }
    }

    //Enemy Knockback function
    public void BounceOffEnemy(Vector2 direction)
    {
        //Give a short invulnerabilty to stop instant damage from collision
        if (invulnTime <= 0)
            invulnTime = 0.05f;
        //Cancel Dash
        dash = false;
        dashTime = 0;
        //Start Bounce
        bounce = true;
        bounceTime = 0.12f;
        bounceDir = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //On Collision reset speed to fix Unity Collision physics.
        rigid2D.velocity = new Vector2(0, 0);
        rigid2D.angularVelocity = 0;
    }

    public void Damage()
    {
        //Take damage if not invulnerable and become invulnerable
        if (invulnTime <= 0)
        {
            audioSource.clip = hurtClip;
            audioSource.Play();
            health--;
            invulnTime = 0.5f;
        }
    }
}
