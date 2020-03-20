using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Health = 25f;
    
    // ===Start====
    void Start()
    {
        
    }

    //====Update====
    void Update()
    {
        
    }
    public void TakeDamage(float amount)
    {
        Health -= amount;
        if(Health <= 0)
        {
            Debug.Log("Enemy has died!");
        }
        Debug.Log("Enemy took some damage");
    }
}
