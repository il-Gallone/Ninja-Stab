using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{
    float timeLeft = 5;

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < 2 && transform.localScale.y < 2)
        {
            transform.localScale += new Vector3(0.5f , 0.5f, 0) * Time.deltaTime;
        }
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
