using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1ProgressManager : MonoBehaviour
{
    public GameObject winScreen, gameUI, barrier;
    private int tags = 0;
    public UnityEngine.UI.Text objectiveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Attack"))
        {
            return;
        }
        if (collider.CompareTag("KeyItem"))
        {
            if (collider.gameObject.GetComponent<KeyItem>().itemName == "ofuda")
            {
                Time.timeScale = 0f;
                PauseController.Paused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                gameUI.SetActive(false);
                winScreen.SetActive(true);
            }
            if (collider.gameObject.GetComponent<KeyItem>().itemName == "tag")
            {
                tags++;
                objectiveText.text = tags + "/4";
                if (tags > 3)
                {
                    Destroy(barrier);
                }
            }
            Destroy(collider.gameObject);
        }
    }
}
