using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string levelName;
    // Start is called before the first frame update
    
    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(levelName);
    }
}
