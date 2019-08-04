using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotationBehaviour : EnemyBase
{


    void Update()
    {
        if (awakened)
        {
            Vector2 targetDirection = transform.position - player.transform.position; //Get Direction to start turning
            targetDirection.Normalize(); //Normallize Direction
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) / Mathf.PI * 180 + 90; //Find the angle of the direction
            transform.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime)); //Rotate Collider slowly at set rate
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
