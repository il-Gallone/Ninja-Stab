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
        Vector2 direction = new Vector2(xAxis, yAxis);
        direction.Normalize();
        if ((xAxis != 0 || yAxis != 0) && !dash)
            angle = Mathf.Atan2(yAxis, xAxis) / Mathf.PI * 180 - 90;
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

        rigid2D.position += direction * speed * Time.deltaTime;
        if (Input.GetKeyDown("joystick 1 button 0") && !bounce && dashCharges > 0)
        {
            dash = true;
            dashTime = 0.18f;
            dashCharges--;
            if(dashCooldown <= 0 && dashCharges < 2)
                dashCooldown = 3f;
        }
        if(Input.GetKeyDown("joystick 1 button 1") && item != "none")
        {
            switch (item)
            {
                case "bolas":
                    item = "none";
                    GameObject bolas = GameObject.Instantiate(bolasPrefab, transform.position, transform.rotation);
                    bolas.GetComponent<BolasProjectile>().direction = new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180));
                    Destroy(bolas, 1.2f);
                    break;
                case "smoke":
                    item = "none";
                    GameObject smoke = GameObject.Instantiate(smokePrefab, transform.position, transform.rotation);
                    smoke.GetComponent<SmokeProjectile>().direction = new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180));
                    Destroy(smoke, 0.5f);
                    break;
                case "boost":
                    item = "none";
                    dashCharges += 3;
                    dashCooldown = 0;
                    break;
            }
        }
        if(dash)
        {
            float dt = Time.deltaTime;
            dashTime -= dt;
            rigid2D.velocity = (new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180)) * 36);
            if (dashTime < 0)
            {
                dt += dashTime;
                dash = false;
                rigid2D.velocity = new Vector2(0, 0);
            }
        }
        if(bounce)
        {
            float dt = Time.deltaTime;
            bounceTime -= dt;
            rigid2D.velocity = bounceDir * 18;
            if (bounceTime < 0)
            {
                dt += bounceTime;
                bounce = false;
                rigid2D.velocity = new Vector2(0, 0);
            }
        }
        if(dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
            if(dashCooldown <= 0)
            {
                dashCharges++;
                if(dashCharges < 2)
                {
                    dashCooldown += 3;
                }
            }
        }
        if(invulnTime >0)
        {
            invulnTime -= Time.deltaTime;
        }
    }

    public void BounceOffEnemy(Vector2 direction)
    {
        if (invulnTime <= 0)
            invulnTime = 0.05f;
        dash = false;
        dashTime = 0;
        bounce = true;
        bounceTime = 0.12f;
        bounceDir = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigid2D.velocity = new Vector2(0, 0);
        rigid2D.angularVelocity = 0;
    }

    public void Damage()
    {
        if (invulnTime <= 0)
        {
            health--;
            invulnTime = 0.5f;
        }
    }
}
