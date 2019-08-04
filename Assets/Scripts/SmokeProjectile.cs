using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeProjectile : MonoBehaviour
{
    public Vector2 direction;
    public GameObject smokePrefab;

<<<<<<< HEAD

=======
>>>>>>> 4440f6b32d6fd03264a5a4967cfcb26f81041701
    private void Update()
    {
        transform.position += (Vector3)direction * 8 * Time.deltaTime;
    }

    private void OnDestroy()
    {
        Instantiate(smokePrefab, transform.position, transform.rotation);
    }
}
    
