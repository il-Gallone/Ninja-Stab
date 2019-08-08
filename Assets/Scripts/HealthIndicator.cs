using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    public GameObject player;
    public Image image;

    private void Start()
    {
        //Find the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Change health appearance with player health variable
        image.fillAmount = player.GetComponent<PlayerController>().health / 4f;
    }
}
