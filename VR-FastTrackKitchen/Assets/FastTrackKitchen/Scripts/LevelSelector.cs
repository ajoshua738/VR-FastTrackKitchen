using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    

    public void LoadLevel(string levelName)
    {
        if (levelName == "Main Menu")
        {
            AudioManager.instance.SetMusicClip(AudioManager.instance.menuBackground);
            SceneManager.LoadScene(levelName);
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
        
    }
}
