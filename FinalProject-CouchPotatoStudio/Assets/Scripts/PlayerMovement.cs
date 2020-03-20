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

    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity;
    private bool isGrounded, doubleJump = true;
    private bool wallRunning, attacking = false;
    private float wallRunDelay;
    private Transform lastWallrun;


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

        if (wallRunning && wallRunDelay < Time.time) //if the player has not moved for more than half a second on a wall they will stop wallrunning
        {
            wallRunning = false;
            wallRunDelay = Time.time + 0.5f;
        }

        //rotate the player to the same direction as the camera when they move if they are not wallrunning
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && !wallRunning)
        {
            transform.localRotation = Quaternion.Euler(0, mainCamera.transform.parent.localRotation.eulerAngles.y, 0);
        }
        //if the player is wallrunning they cannot move sideways or backwards
        if (wallRunning)
        {
            x = 0;
            if (z < 0)
            {
                z = 0;
            }
            else if (z > 0) //if the player has moved while wallrunning reset the time they can stay on the wall
            {
                z *= 1.5f; //increased speed while wallrunning
                wallRunDelay = Time.time + 0.5f;
            }
        }
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //========Jumping code============
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.55f,0.1f, 0.55f), Quaternion.Euler(0,45,0), groundMask);
        if ((isGrounded && velocity.y < 0) || wallRunning) //how far they can jump.. i think :P
        {
            velocity.y = -2f;
            doubleJump = true;
        }
        if (!wallRunning)
        {
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
        else
        {
            if (Input.GetButtonDown("Jump")) //wall jump
            {
                wallRunning = false;
                wallRunDelay = Time.time + 0.25f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                velocity += transform.right * (jumpHeight * -5 * Mathf.Sign(transform.InverseTransformPoint(lastWallrun.position).x));
            }
        }
        //reduce the horizontal force from wall jumping over time
        if (Mathf.Abs(velocity.x) > 0)
        {
            velocity.x -= (velocity.x + 2f) * Time.deltaTime * 2 + Mathf.Sign(velocity.x) * 0.1f;
            if (Mathf.Abs(velocity.x) < 1)
            {
                velocity.x = 0;
            }
        }
        if (Mathf.Abs(velocity.z) > 0)
        {
            velocity.z -= (velocity.z + 2f) * Time.deltaTime * 2 + Mathf.Sign(velocity.z) * 0.1f;
            if (Mathf.Abs(velocity.z) < 1)
            {
                velocity.z = 0;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Wallrun") && !wallRunning && wallRunDelay < Time.time)
        {
            if (lastWallrun != null)
            {
                if (collider.gameObject == lastWallrun.gameObject && wallRunDelay + 0.75f > Time.time)
                {
                    return; //if the player has used the same surface to wallrun in the last second, do not wallrun
                }
            }
            //determine which angle to wallrun at
            if (Mathf.Abs(gameObject.transform.rotation.eulerAngles.y - collider.transform.rotation.eulerAngles.y) < 90 || Mathf.Abs(gameObject.transform.rotation.eulerAngles.y - collider.transform.rotation.eulerAngles.y) > 270)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, collider.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, collider.transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
            }
            wallRunning = true;
            wallRunDelay = Time.time + 0.5f;
            lastWallrun = collider.transform;
            velocity.x = 0;
            velocity.z = 0;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Wallrun") && wallRunning)
        {
            wallRunning = false;
            wallRunDelay = Time.time + 0.25f;
        }
    }
}