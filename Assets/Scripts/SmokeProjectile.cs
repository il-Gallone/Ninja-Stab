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

    IEnumerator SmokesOut()
    {
        for(float i = 0; i < 0.5; i+= Time.deltaTime)
        {
            transform.position += (Vector3)direction * 8 * Time.deltaTime;
            yield return null;
        }
        Instantiate(smokePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
