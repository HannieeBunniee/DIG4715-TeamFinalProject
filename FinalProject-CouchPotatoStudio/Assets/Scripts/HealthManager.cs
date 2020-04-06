using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject gameUI;
    public GameObject winScreen, loseScreen;
    private bool death = false;
    private float iFrames;

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
            Time.timeScale = 0f;
            PauseController.Paused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameUI.SetActive(false);
            winScreen.SetActive(true);
        }

        //Player HP
        if (!death && currentHealth == 0)
        {
            death = true;
            PauseController.Paused = true;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameUI.SetActive(false);
            loseScreen.SetActive(true);
        }
    }

    //====Function====
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetPlayerHealth(currentHealth);
        iFrames = Time.time + 0.25f;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (iFrames < Time.time && collider.CompareTag("Enemy"))
        {
            int damage = collider.gameObject.GetComponent<EnemyController>().contactDamage;
            if (damage > 0)
            {
                TakeDamage(damage);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (iFrames < Time.time && collision.collider.CompareTag("Enemy"))
        {
            int damage = collision.collider.gameObject.GetComponent<EnemyController>().contactDamage;
            if (damage > 0)
            {
                TakeDamage(damage);
            }
        }
    }
}
