using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    //public GameObject Scene;
    public int destination = 0; //the target stage to teleport to
    public int currentStage = 0; //the current stage
    public bool stageComplete = false; //if the current stage has been successfully completed

    private void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadStage();
        }
    }

    public void LoadStage()
    {
        if (stageComplete)
        {
            switch (currentStage) //if the current stage was completed, mark it as complete
            {
                case 1:
                    PlayerPrefs.SetInt("stage1", 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("stage2", 2);
                    break;
                case 3:
                    PlayerPrefs.SetInt("stage3", 3);
                    break;
            }
        }
        PlayerPrefs.SetInt("lastStage", currentStage); //set the previous stage visited to the current stage, allowing different spawn points when re-entering the hub
        switch (destination) //load the destination level
        {
            case 0:
                SceneManager.LoadScene("HUB");
                break;
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
            case 3:
                SceneManager.LoadScene("Stage3");
                break;
        }
    }
}
