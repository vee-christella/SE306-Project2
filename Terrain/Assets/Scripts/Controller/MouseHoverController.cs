using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Retrieved from https://www.youtube.com/watch?v=7ybz28Py0-U&t=1s
Class to get the GameObject under the player's cursor point.
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

    void Update()
    {
        if (GameController.Instance.Game.HasStarted)
        {
            // Only GameObjects with box colliders will be detected under the cursor
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out theObject))
            {
                selectedObject = theObject.transform.gameObject.name;
                internalObject = theObject.transform.gameObject.name;
                mousePoint = theObject.point;
            }
        }
    }

}
