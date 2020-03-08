using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float zoom = 5f;
    public Transform parent, player;

    private float xRotation = 0f;
    private float yRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false; //setting cursor visible or not
        Cursor.lockState = CursorLockMode.Locked; // freeze the cursor in middle of the screen so it wont move
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseY * PlayerPrefs.GetInt("InvertedY", -1);
        xRotation = Mathf.Clamp(xRotation, -15, 90);
        yRotation += mouseX * PlayerPrefs.GetInt("InvertedX", 1);
        yRotation %= 360;

        parent.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        parent.position = player.transform.position - parent.forward * zoom + Vector3.up * 2;
    }
}