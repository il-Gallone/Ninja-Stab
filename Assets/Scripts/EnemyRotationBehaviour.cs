using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotationBehaviour : EnemyBase
{
    public float rotationSpeed = 120f;
    

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

    public override void BolasAttack()
    {
        rotationSpeed /= 2;
        awakened = true;
        bolasTime = 5f;
    }

    public override void Backstab()
    {
        //Do Nothing
    }
}
