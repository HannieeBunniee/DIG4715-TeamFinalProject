using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject loseScreen;
    public GameObject winScreen;
    private bool death = false;

    //====Start====
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetPlayerHealth(maxHealth);
    }

    // =====Update====
    void Update()
    {
        //Testing
        if(Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(20);
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            Time.timeScale = 0f; //freeze the game //THIS FREEZE THE GAME BUT WONT UNFREEZE IT WHEN PRESSED RESTART
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; //unlock cursorLock so they can click buttons
            winScreen.SetActive(true);
        }

        //Player HP
        if (!death && currentHealth == 0)
        {
            death = true;
            Time.timeScale = 0f; //freeze the game //THIS FREEZE THE GAME BUT WONT UNFREEZE IT WHEN PRESSED RESTART
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; //unlock cursorLock so they can click buttons
            //Destroy(gameObject); //change this later on pls, this will break the game cuz it destroy the whole script folder
            loseScreen.SetActive(true);
        }
    }

    //====Function====
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetPlayerHealth(currentHealth);
    }
}
