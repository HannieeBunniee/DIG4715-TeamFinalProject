using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePanelUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*//grabing the GameObject with the gamestatus script in it
        GameObject go = GameObject.Find("GameStatus");
        if (go == null)
        {
            Debug.LogError("Failed to find an object named 'GameStatus'");
            this.enabled = false;
            return;
        }
        //the gamestatus script
        GameStatus gs = go.GetComponent<GameStatus>();
        GetComponent<Text>().text = "Stage Completed: " + gs.stageComplete;*/
        GetComponent<Text>().text = "Stage Completed: " + GameStatus.stageComplete;
    }
}
