using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject hintText;

    //====Start===
    void Start()
    {
        
    }

    // ====Updates====
    void Update()
    {
        
    }
    //=====Colliders====
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.SetActive(false);
        }
    }
}
