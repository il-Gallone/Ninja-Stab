using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotationBehaviour : EnemyBase
{


    void Update()
    {
        if (awakened)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z, AngleFinder(), rotationSpeed * Time.deltaTime)); //Rotate Collider slowly at set rate
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
