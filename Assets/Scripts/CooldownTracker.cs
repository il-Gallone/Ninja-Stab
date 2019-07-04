using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTracker : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteMask>().alphaCutoff = Mathf.InverseLerp(3, 0, 3 - player.GetComponent<PlayerController>().dashCooldown);
    }
}
