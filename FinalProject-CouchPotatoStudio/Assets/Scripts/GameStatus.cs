using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    static public int stageComplete = 0;
    public bool stage1;
    public bool stage2;

    //enable next stage once the previous stage is completed
    public GameObject stage3Enable;

    //====Start===
    void Start()
    {
        stage3Enable.SetActive(false);
    }

    // ====Updates====
    void Update()
    {
        //testing the stage
        if(Input.GetKeyDown(KeyCode.X))
        {
            stageComplete = stageComplete + 1;
        }

        
        //Enable Final Stage
        if (stageComplete == 2)
        {
            stage3Enable.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stage1 == true)
        {
            stageComplete = stageComplete + 1;
            GetComponent<Collider>().enabled = false;
        }

        else if (other.CompareTag("Player") && stage2 == true)
        {
            stageComplete = stageComplete + 1;
            GetComponent<Collider>().enabled = false;
        }
    }

        //Checking to see if the script get destroy and recreated while switching scene
    private void OnDestroy()
    {
        Debug.Log("GameStatus was Destroyed.");
    }
}
