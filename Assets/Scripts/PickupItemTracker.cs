using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItemTracker : MonoBehaviour
{
    public GameObject player;
    public Image display;
    public Sprite bolas;
    public Sprite smoke;
    public Sprite boost;
    

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().item == "none")
        {
            display.color = new Color(1, 1, 1, 0);
        }
        else
        {
            display.color = new Color(1, 1, 1, 1);
            if(player.GetComponent<PlayerController>().item == "bolas")
            {
                display.sprite = bolas;
            }
            if (player.GetComponent<PlayerController>().item == "smoke")
            {
                display.sprite = smoke;
            }
        }
    }
}
