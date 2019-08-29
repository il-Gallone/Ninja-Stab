using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string levelName;
    public void LoadLevel()
    {
        GameManager.instance.checkpoint = 0;
        SceneManager.LoadSceneAsync(levelName);
    }
    public void LoadLevelFromCheckpoint()
    {
        SceneManager.LoadSceneAsync(levelName);
    }
}
