using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Retrieved from https://answers.unity.com/questions/666905/in-game-camera-movement-like-editor.html

Controller for the main camera
*/
public class CameraController : MonoBehaviour
{


    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float zoomSpeed = 2f;
    public float dragSpeed = 6f;

    private float yaw = 10f;
    private float pitch = 25f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(6, 4, -2);
        transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }


    void Update()
    {
        // Look around with Right Mouse
        if (Input.GetMouseButton(1))
        {
            yaw += lookSpeedH * Input.GetAxis("Mouse X");
            pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }

        // Drag camera around with Middle Mouse
        if (Input.GetMouseButton(2))
        {
            transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);
        }

        // w to move camera forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        // s to move camera backwards
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        // a to move camera left
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        // d to move camera right
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        // Zoom in and out with Mouse Wheel
        transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
    }

}
