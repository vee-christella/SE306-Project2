using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float zoomSpeed = 2f;
    public float dragSpeed = 6f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        //Look around with Right Mouse
        if (Input.GetMouseButton(1))
        {
            yaw += lookSpeedH * Input.GetAxis("Mouse X");
            pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }

        //drag camera around with Middle Mouse
        if (Input.GetMouseButton(2))
        {
            transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);
        }

        //Zoom in and out with Mouse Wheel
        transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
    }


    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     // Zoom in
    //     if (Input.GetAxis("Mouse ScrollWheel") > 0)
    //     {
    //         Debug.Log("Zooming in");
    //         GetComponent<Transform>().position = new Vector3(
    //             transform.position.x,
    //             transform.position.y - 0.05f,
    //             transform.position.z + 0.3f
    //         );
    //     }

    //     // Zoom out
    //     if (Input.GetAxis("Mouse ScrollWheel") < 0)
    //     {
    //         Debug.Log("Zooming out");
    //         GetComponent<Transform>().position = new Vector3(
    //             transform.position.x,
    //             transform.position.y + 0.05f,
    //             transform.position.z - 0.3f
    //         );
    //     }
    // }


}
