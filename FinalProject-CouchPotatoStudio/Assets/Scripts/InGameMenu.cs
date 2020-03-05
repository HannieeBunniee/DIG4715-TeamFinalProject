using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool winLoseCondition = false;
    public GameObject pauseMenuUI;

    private bool optionOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && winLoseCondition == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else if (optionOn == false)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //unfreeze the game
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //freeze the game
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        //Debug.Log("Loading Menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("quitting game from Hub");
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("HUB");
        Time.timeScale = 1f; //unfreeze the game
    }

    public void optionMenu()
    {
        optionOn = true;
    }

    public void backButton()
    {
        optionOn = false;
    }
}
