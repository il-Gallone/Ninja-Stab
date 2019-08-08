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
        //Change Canvas text to the number of player dashes.
        text.text =  player.GetComponent<PlayerController>().dashCharges.ToString();
    }
}
