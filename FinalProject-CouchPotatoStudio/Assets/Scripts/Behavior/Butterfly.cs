using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    private bool attackMode = false;
    private PlayerMovement player;
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject candidate in playerObjects)
        {
            if (candidate.GetComponent<PlayerMovement>() != null)
            {
                player = candidate.GetComponent<PlayerMovement>();
            }
        }
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 30)
        {
            attackMode = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackMode)
        {
            transform.LookAt(Vector3.Lerp(transform.forward, player.transform.position, 0.4f));
            body.AddForce(transform.forward * 20);
        }
        else
        {
            transform.Rotate(new Vector3(0, 60 * Time.deltaTime, 0));
            body.AddForce(transform.forward * 20);
        }
        if (body.velocity.magnitude > 15)
        {
            body.velocity *= 15 / body.velocity.magnitude;
        }
    }
}
