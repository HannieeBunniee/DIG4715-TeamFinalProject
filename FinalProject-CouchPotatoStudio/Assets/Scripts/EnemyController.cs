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
            Debug.Log("2");
            health--;
            damageDelay = player.comboTime + 0.1f;
            player.airTime = player.comboTime;
        }
    }
}
