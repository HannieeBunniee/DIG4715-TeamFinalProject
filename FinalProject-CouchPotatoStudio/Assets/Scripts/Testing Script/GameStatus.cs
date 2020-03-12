using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    static public int stageComplete = 0;

    //====Start===
    void Start()
    {

    }

    // ====Updates====
    void Update()
    {
        //testing the stage
        if(Input.GetKeyDown(KeyCode.X))
        {
            stageComplete = stageComplete + 1;
        }

    }

    //Checking to see if the script get destroy and recreated while switching scene
    private void OnDestroy()
    {
        Debug.Log("GameStatus was Destroyed.");
    }
}
