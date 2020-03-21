using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitController : MonoBehaviour
{
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
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<CharacterController>().enabled = false;
            collider.transform.position = collider.GetComponent<PlayerMovement>().lastSafePosition;
            collider.GetComponent<CharacterController>().enabled = true;
        }
    }
}
