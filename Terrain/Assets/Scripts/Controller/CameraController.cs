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
        // Tutorial Scene
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            transform.position = new Vector3(2, 2, -2);
            transform.eulerAngles = new Vector3(30f, 0, 0);
        }
        else
        {
            transform.position = new Vector3(6, 4, -2);
            transform.eulerAngles = new Vector3(pitch, yaw, 0);
        }
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

        // Zoom in and out with Mouse Wheel
        transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
    }

}
