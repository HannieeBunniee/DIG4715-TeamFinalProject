using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject stage2Enable;
    public GameObject stage3Enable;

    //====Start===
    void Start()
    {
        stage2Enable.SetActive(false);
        stage3Enable.SetActive(false);
    }

    // ====Updates====
    void Update()
    {
        //need to call the GameStatus Script for it stagecomplete int 

        //grabing the GameObject with the gamestatus script in it
        GameObject go = GameObject.Find("GameStatus");
        if (go == null)
        {
            Debug.LogError("Failed to find an object named 'GameStatus'");
            this.enabled = false;
            return;
        }
        //the gamestatus script
        GameStatus gs = go.GetComponent<GameStatus>();
        if (gs.stageComplete = 1)
        {
            stage2Enable.SetActive(true);
        }
        if (gs.stageComplete = 2)
        {
            stage3Enable.SetActive(true);
        }
    }
}
