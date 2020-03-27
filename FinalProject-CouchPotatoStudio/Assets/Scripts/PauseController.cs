using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool Paused = false;
    public static bool winLoseCondition = false;
    public GameObject pauseMenuUI, optionUI, gameUI;
    private float delay;
    public UnityEngine.EventSystems.EventSystem eventSystem;

    void Update()
    {
        if (Input.GetButton("Pause") && !winLoseCondition && delay + 0.5f < Time.unscaledTime)
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        delay = Time.unscaledTime;
        gameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        optionUI.SetActive(false);
        Time.timeScale = 1f; //unfreeze the game
        Paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; //lock cursorLock after menu
    }

    void Pause()
    {
        eventSystem.SetSelectedGameObject(pauseMenuUI.transform.GetChild(0).gameObject);
        delay = Time.unscaledTime;
        gameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //freeze the game
        Paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; //unlock cursorLock so they can click buttons
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu");
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }
}
