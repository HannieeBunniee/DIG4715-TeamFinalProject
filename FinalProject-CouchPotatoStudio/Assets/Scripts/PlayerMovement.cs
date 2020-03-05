using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //==Movement==
    public CharacterController controller;
    public float speed = 12f;

    //==Jump==
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck; //make an empty game object and place it in the bottom of player ;)
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    private bool isGrounded;


    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //========Moving code=============
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //========Jumping code============
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) //how far they can jump.. i think :P
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) //code for jump only if they are grounded
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}