using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject mainCamera;

    //==Movement==
    public CharacterController controller;
    public float speed = 12f;

    //==Jump==
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck; //make an empty game object and place it in the bottom of player ;)
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Vector3 velocity;
    private bool isGrounded, doubleJump = true;


    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //========Moving code=============
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //rotate the player to the same direction as the camera when they move
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            transform.localRotation = Quaternion.Euler(0, mainCamera.transform.parent.localRotation.eulerAngles.y, 0);
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //========Jumping code============
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.55f,0.1f, 0.55f), Quaternion.Euler(0,45,0), groundMask);
        if (isGrounded && velocity.y < 0) //how far they can jump.. i think :P
        {
            velocity.y = -2f;
            doubleJump = true;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || doubleJump)) //code for jump
        {
            if (isGrounded) //use the first jump
            {
                isGrounded = false;
            }
            else //use the double jump
            {
                doubleJump = false;
            }
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        if (velocity.y < gravity) //cap the falling speed
        {
            velocity.y = gravity;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}