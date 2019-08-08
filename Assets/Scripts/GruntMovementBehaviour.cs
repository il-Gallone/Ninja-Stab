using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntMovementBehaviour : EnemyBase
{
    private void Start()
    {
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (awakened)
        {
            //Only move if not under the effects of a bolas
            if (bolasTime <= 0)
            {
                if (player.GetComponent<PlayerController>().dashCharges > 1)//Run away if the player has 2 or more dashes
                {
                    Flee();
                }
                else//Run toward if the player has 1 or fewer dashes
                {
                    Chase();
                }
            }
        }
    }
    
}
