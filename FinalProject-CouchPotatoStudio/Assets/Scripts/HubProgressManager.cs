using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubProgressManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("stage1", 0) == 1 && PlayerPrefs.GetInt("stage2", 0) == 1 && PlayerPrefs.GetInt("stage3", 0) == 1 && PlayerPrefs.GetInt("seenEnd", 0) == 0)
        {
            PlayerPrefs.SetInt("seenEnd", 1);
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
