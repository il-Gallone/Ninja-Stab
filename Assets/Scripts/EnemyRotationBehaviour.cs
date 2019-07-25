﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotationBehaviour : EnemyBase
{
    public float rotationSpeed = 90f;


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
            Vector2 targetDirection = transform.position - player.transform.position;
            targetDirection.Normalize();
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) / Mathf.PI * 180 + 90;
            transform.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime));
        }
    }
    

    public override void Backstab()
    {
        //Do Nothing
    }
    public override void BolasAttack()
    {
        rotationSpeed /= 2;
        awakened = true;
        bolasTime = 5f;
    }
}
