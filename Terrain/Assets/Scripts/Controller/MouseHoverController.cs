using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Retrieved from https://www.youtube.com/watch?v=7ybz28Py0-U&t=1s
 */
public class MouseHoverController : MonoBehaviour
{
    public static string selectedObject;
    public string internalObject;
    public RaycastHit theObject;
    private Camera mainCamera;
    public Vector3 mousePoint;

    void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out theObject))
        {
            selectedObject = theObject.transform.gameObject.name;
            internalObject = theObject.transform.gameObject.name;
            mousePoint = theObject.point;
        }
    }
}
