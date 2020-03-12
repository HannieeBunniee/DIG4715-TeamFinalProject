using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCompleteCheck : MonoBehaviour
{
    public static bool stage1Completed;

    private void Start()
    {
        stage1Completed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stage1Completed = true;
        }
        
    }
}
