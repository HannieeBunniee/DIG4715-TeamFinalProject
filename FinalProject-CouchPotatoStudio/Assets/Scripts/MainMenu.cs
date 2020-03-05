using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f; //unfreeze the game (incase they decided to play AFTER going main menu from pause menu)
        SceneManager.LoadScene("HUB");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting From Main");
        Application.Quit();
    }
}
