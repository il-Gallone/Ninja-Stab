using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeTracker : MonoBehaviour
{
    public GameObject player;
    public Text text;

    // Update is called once per frame
    void Update()
    {
        text.text =  '=' + player.GetComponent<PlayerController>().dashCharges.ToString();
        if (player.GetComponent<PlayerController>().dashCharges == 0)
        {
            text.text = "0";
        }
    }
}
