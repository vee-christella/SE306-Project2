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
    public float dragSpeed = 6f;
    public float wasdSpeed = 0.5f;

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
            // transform.Translate(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            transform.TransformDirection(Vector3.forward);
            Vector3 Forward = transform.forward * Input.GetAxis("Vertical") * wasdSpeed;
            Vector3 Right = transform.right * Input.GetAxis("Horizontal") * wasdSpeed;
            Forward.y = 0;
            transform.position += Forward + Right;
        }
        // s to move camera backwards
        else if (Input.GetKey(KeyCode.S))
        {
            transform.TransformDirection(Vector3.back);
            Vector3 Backward = transform.forward * Input.GetAxis("Vertical") * wasdSpeed;
            Vector3 Right = transform.right * Input.GetAxis("Horizontal") * wasdSpeed;
            Backward.y = 0;
            transform.position += Backward + Right;
        }
        // a to move camera left
        else if (Input.GetKey(KeyCode.A))
        {
            // transform.Translate(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            transform.TransformDirection(Vector3.left);
            Vector3 Left = transform.forward * Input.GetAxis("Vertical") * wasdSpeed;
            Vector3 Right = transform.right * Input.GetAxis("Horizontal") * wasdSpeed;
            Left.y = 0;
            transform.position += Left + Right;
        }
        // d to move camera right
        else if (Input.GetKey(KeyCode.D))
        {
            // transform.Translate(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            transform.TransformDirection(Vector3.right);
            Vector3 Rite = transform.forward * Input.GetAxis("Vertical") * wasdSpeed;
            Vector3 Right = transform.right * Input.GetAxis("Horizontal") * wasdSpeed;
            Rite.y = 0;
            transform.position += Rite + Right;
        }

        // Zoom in and out with Mouse Wheel
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if ((transform.position.y > 1 && Input.GetAxis("Mouse ScrollWheel") > 0) || (transform.position.y < 15 && Input.GetAxis("Mouse ScrollWheel") < 0))
        {
            transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);

            if (transform.position.y < 1)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.y);
            }
        }
    }

}
