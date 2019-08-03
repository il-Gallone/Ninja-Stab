using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    public GameObject player;
    public SpriteMask mask;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        mask.alphaCutoff = 1 - player.GetComponent<PlayerController>().health / 4f;
    }
}
