using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.paused == true)
        {
            pauseMenu.enabled = true;
        }
        else
        {
            pauseMenu.enabled = false;
        }
    }

    public void Resume()
    {
        GameManager.instance.paused = false;
        Time.timeScale = 1f;
    }
}
