using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotationBehaviour : MonoBehaviour
{
    GameObject player;
    public Rigidbody2D rigid2D;
    bool awakened = false;
    public float rotationSpeed = 120f;
    float bolasTime = 0;

    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (awakened)
        {
            if (bolasTime > 0)
            {
                bolasTime -= Time.deltaTime;
                if (bolasTime <= 0)
                {
                    rotationSpeed *= 2;
                }
            }
            Vector2 targetDirection = rigid2D.position - (Vector2)player.transform.position;
            targetDirection.Normalize();
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) / Mathf.PI * 180 + 90;
            rigid2D.rotation = Mathf.MoveTowardsAngle(rigid2D.rotation, targetAngle, rotationSpeed * Time.deltaTime);
        }
    }

    public void BolasAttack()
    {
        awakened = true;
        rotationSpeed /= 2;
        bolasTime = 5f;
    }

    public void Alert()
    {
        awakened = true;
    }
}
