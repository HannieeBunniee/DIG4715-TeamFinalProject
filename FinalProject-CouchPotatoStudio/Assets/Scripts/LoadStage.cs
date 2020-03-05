using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStage : MonoBehaviour
{
    //public GameObject Scene;
    public bool stage1;
    public bool stage2;
    public bool stage3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stage1 == true)
        {
            SceneManager.LoadScene("Stage1");
        }
        if (other.CompareTag("Player") && stage2 == true)
        {
            SceneManager.LoadScene("Stage2");
        }
        if (other.CompareTag("Player") && stage3 == true)
        {
            SceneManager.LoadScene("Stage3");
        }
    }
}
