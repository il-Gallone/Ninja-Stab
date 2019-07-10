using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeProjectile : MonoBehaviour
{
    public Vector2 direction;
    public GameObject smokePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SmokesOut");
    }

    private void Update()
    {
        transform.position += (Vector3)direction * 8 * Time.deltaTime;
    }

    private void OnDestroy()
    {
        Instantiate(smokePrefab, transform.position, transform.rotation);
    }
}
    
