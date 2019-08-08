using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Move based on player Y
        transform.position = new Vector3(0, player.transform.position.y, -10);
    }
}
