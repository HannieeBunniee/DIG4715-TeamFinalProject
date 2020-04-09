using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    private bool attackMode = false;
    private PlayerMovement player;
    private Rigidbody body;
    public float turn, speed, detectionRange;
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
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            attackMode = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackMode)
        {
            transform.LookAt(Vector3.Lerp(transform.position + transform.forward, player.transform.position, 0.05f * Time.deltaTime));
            Debug.DrawLine(transform.position, player.transform.position);
            body.velocity = transform.forward * speed;
        }
        else
        {
            transform.Rotate(new Vector3(0, turn * Time.deltaTime, 0));
            body.velocity = transform.forward * speed;
        }
        if (body.velocity.magnitude > speed)
        {
            body.velocity = body.velocity.normalized * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        transform.Rotate(0, 180, 0);
    }
}
