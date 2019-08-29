using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //The GameManager Object
    public static GameManager instance;

    public int level = 1;
    public int checkpoint = 0;
    public bool paused = false;
    public bool playerInLevel = false;

    private void Awake()
    {
        //If the GameManager is the original stay around otherwise destroy itself
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //Don't destroy when loading scene
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerInLevel = true;
        }
        else
        {
            playerInLevel = false;
        }
        if (playerInLevel)
        {
            if(Input.GetKeyDown("joystick 1 button 7"))
            {
                if(paused)
                {
                    paused = false;
                    Time.timeScale = 1f;
                }
                else
                {
                    paused = true;
                    Time.timeScale = 0f;
                }
            }
        }
    }
}
