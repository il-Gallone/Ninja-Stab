using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeProjectile : MonoBehaviour
{
    public Vector2 direction;
    public GameObject smokePrefab;
    
    private void Update()
    {
        transform.position += (Vector3)direction * 8 * Time.deltaTime;
    }

    private void OnDestroy()
    {
        Instantiate(smokePrefab, transform.position, transform.rotation);
    }
}
    
