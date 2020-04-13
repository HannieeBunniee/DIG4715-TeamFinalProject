using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailForce : MonoBehaviour
{
    private Rigidbody body;
    private Vector3 parentPosLastFrame = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.AddForce((parentPosLastFrame - transform.parent.position) * 10);
        parentPosLastFrame = transform.parent.position;
    }
}
