using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    private float damageDelay = 0f;
    private PlayerMovement player;
    
    // ===Start====
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject candidate in playerObjects)
        {
            if (candidate.GetComponent<PlayerMovement>() != null)
            {
                player = candidate.GetComponent<PlayerMovement>();
            }
        }
    }

    //====Update====
    void Update()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Attack") && player.attacking && damageDelay < Time.time)
        {
            health--;
            damageDelay = player.comboTime + 0.1f;
            player.airTime = player.comboTime + 0.25f;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Attack") && player.dashing && damageDelay < Time.time)
        {
            StartCoroutine(DelayedDamage());
            damageDelay = Time.time + 0.1f;
        }
    }

    IEnumerator DelayedDamage() //wait to deal damage from a dash to prevent enemy from dying before dash ends and throwing an error
    {
        yield return new WaitForSeconds(0.25f);
        health--;
        yield break;
    }
}
