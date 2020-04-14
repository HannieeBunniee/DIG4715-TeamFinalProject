using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject gameUI;
    public GameObject loseScreen;
    private Animator animator;
    public bool death = false;
    public float iFrames;
    public AudioSource music;

    //====Start====
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetPlayerHealth(maxHealth);
    }

    // =====Update====
    void Update()
    {
        //Player HP
        if (!death && currentHealth < 1)
        {
            death = true;
            animator.SetTrigger("Death");
            gameObject.GetComponent<PlayerMovement>().controlsLocked = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameUI.SetActive(false);
            StartCoroutine(Die());
        }
    }

    //====Function====
    void TakeDamage(int damage)
    {
        animator.SetTrigger("Damage");
        currentHealth -= damage;
        healthBar.SetPlayerHealth(currentHealth);
        iFrames = Time.time + 0.25f;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (iFrames < Time.time && collider.CompareTag("Enemy") && !death)
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
        if (iFrames < Time.time && collision.collider.CompareTag("Enemy") && !death)
        {
            int damage = collision.collider.gameObject.GetComponent<EnemyController>().contactDamage;
            if (damage > 0)
            {
                TakeDamage(damage);
            }
        }
    }

    IEnumerator Die()
    {
        StartCoroutine(FadeMusic());
        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().cameraLocked = true;
        loseScreen.SetActive(true);
        Image loseBG = loseScreen.GetComponent<Image>();
        float a = loseBG.color.a;
        for (int i = 0; i < 181; i++)
        {
            loseBG.color = new Color(loseBG.color.r, loseBG.color.g, loseBG.color.b, i / 120f * a);
            for (int j = 0; j < loseScreen.transform.childCount; j++)
            {
                Transform child = loseScreen.transform.GetChild(j);
                Text text;
                if (child.gameObject.TryGetComponent(out text))
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, i / 180f);
                }
                else if (child.childCount != 0)
                {
                    text = child.GetChild(0).gameObject.GetComponent<Text>();
                    text.color = new Color(text.color.r, text.color.g, text.color.b, i / 180f);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator FadeMusic()
    {
        for (int i = 100; i > -1; i--)
        {
            music.volume = i / 100f;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
