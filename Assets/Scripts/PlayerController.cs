using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float speed = 4;
    float angle = 0;
    public bool dash = false;
    float dashTime = 0;
    public int dashCharges = 2;
    public float dashCooldown = 0;
    bool bounce = false;
    float bounceTime = 0;
    Vector2 bounceDir;
    public string item = "none";
    public GameObject bolasPrefab;
    public GameObject smokePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal"); //Change Horizontal to X-axis when switching to PS controller
        float yAxis = Input.GetAxis("Vertical"); //Change Vertical to Y-axis when switching to PS controller
        Vector2 direction = new Vector2(xAxis, yAxis);
        direction.Normalize();
        if ((xAxis != 0 || yAxis != 0 )&& !dash)
            angle = Mathf.Atan2(yAxis, xAxis)/Mathf.PI*180-90;
        transform.eulerAngles = new Vector3(0, 0, angle);

        rigid2D.position += direction * speed * Time.deltaTime;
        if (Input.GetKeyDown("joystick 1 button 0") && !bounce && dashCharges > 0)
        {
            dash = true;
            dashTime = 0.18f;
            dashCharges--;
            if(dashCooldown <= 0 && dashCharges < 2)
                dashCooldown = 3.0f;
        }
        if(Input.GetKeyDown("joystick 1 button 1") && item != "none")
        {
            switch (item)
            {
                case "bolas":
                    item = "none";
                    GameObject bolas = GameObject.Instantiate(bolasPrefab, transform.position, transform.rotation);
                    bolas.GetComponent<BolasProjectile>().direction = new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180));
                    Destroy(bolas, 3f);
                    break;
                case "smoke":
                    item = "none";
                    GameObject smoke = GameObject.Instantiate(smokePrefab, transform.position, transform.rotation);
                    smoke.GetComponent<SmokeProjectile>().direction = new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180));
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
            rigid2D.velocity = (new Vector2(Mathf.Cos((angle + 90) * Mathf.PI / 180), Mathf.Sin((angle + 90) * Mathf.PI / 180)) * 24);
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
            rigid2D.velocity = bounceDir * 12;
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
    }

    public void BounceOffEnemy(Vector2 direction)
    {
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
}
