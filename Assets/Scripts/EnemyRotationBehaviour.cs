using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotationBehaviour : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rigid2D;
    public float rotationSpeed = 1.5f;
    
    // Update is called once per frame
    void Update()
    {
        Vector2 targetDirection = rigid2D.position - (Vector2)player.transform.position;
        targetDirection.Normalize();
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) / Mathf.PI * 180 +90;
        rigid2D.rotation = Mathf.LerpAngle(rigid2D.rotation, targetAngle, rotationSpeed * Time.deltaTime);
    }

    public void BolasAttack()
    {
        StartCoroutine("RotateSlow");
    }

    IEnumerator RotateSlow()
    {
        rotationSpeed /= 2;
        yield return new WaitForSeconds(5);
        rotationSpeed *= 2;
    }
}
