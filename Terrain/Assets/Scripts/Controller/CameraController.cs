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
    public float zoomSpeed = 1f;
    public float rotateSpeed = 80f;
    public float wasdSpeed = 0.1f;
    private float pitch = 25f;
    private float yaw = 10f;

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
            Vector3 rotation = transform.eulerAngles;
            pitch = rotation.x - lookSpeedV * Input.GetAxis("Mouse Y");
            yaw = rotation.y + lookSpeedH * Input.GetAxis("Mouse X");

            transform.eulerAngles = new Vector3(pitch, yaw, rotation.z);
        }

        // Move camera forward/back/left/right with wasd respectively
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.TransformDirection(Vector3.forward);

            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.TransformDirection(Vector3.back);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.TransformDirection(Vector3.left);

            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.TransformDirection(Vector3.right);

            }

            Vector3 forwardback = transform.forward * Input.GetAxis("Vertical") * wasdSpeed;
            Vector3 leftright = transform.right * Input.GetAxis("Horizontal") * wasdSpeed;
            forwardback.y = 0;
            transform.position += forwardback + leftright;
        }

        // q to rotate camera left
        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.y -= rotateSpeed * Time.deltaTime;
            transform.eulerAngles = rotation;
        }
        // e to rotate camera right
        else if (Input.GetKey(KeyCode.E))
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.y += rotateSpeed * Time.deltaTime;
            transform.eulerAngles = rotation;
        }

        // Zoom in and out with Mouse Wheel
        if ((transform.position.y > 1 && Input.GetAxis("Mouse ScrollWheel") > 0) || (transform.position.y < 15 && Input.GetAxis("Mouse ScrollWheel") < 0))
        {
            transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
        }
    }

}
