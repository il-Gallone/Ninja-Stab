using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownTracker : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        //Find the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Change the cooldown display based on player's cooldown variable
        gameObject.GetComponent<Image>().fillAmount = 1 - Mathf.InverseLerp(3, 0, 3 - player.GetComponent<PlayerController>().dashCooldown);
    }
}
