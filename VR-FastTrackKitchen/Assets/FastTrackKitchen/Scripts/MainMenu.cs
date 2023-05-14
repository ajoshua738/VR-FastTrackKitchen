using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        AudioManager.instance.SetMusicClip(AudioManager.instance.gameBackground);
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
