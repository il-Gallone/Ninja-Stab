using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteColorController : MonoBehaviour
{
    GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //find the parent
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //change colour of the dynamite based on the kamikaze's fusetime variable
        if(parent.GetComponent<KamikazeMovementBehaviour>().fuseLit && parent.GetComponent<KamikazeMovementBehaviour>().fuseTime <=4)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, parent.GetComponent<KamikazeMovementBehaviour>().fuseTime / 4, 0);
        }
    }
}
