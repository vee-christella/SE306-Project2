﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
Code is from quill18creates youtube Channel, "Unity Base-Building Game Tutorial - Episode 4!"
*/
public class MouseController : MonoBehaviour
{
    private GameGrid gameGrid;
    private Camera mainCamera;
    private string buildingForCreating = null;
    public Text cancelButtonString;
    Vector3 lastFramePosition;

    private void Awake()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 currFramePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //If mouse over a UI element, bail out
        // if (EventSystem.current.IsPointerOverGameObject())
        // {
        //     return;
        // }

        // if (Input.GetMouseButton(1))
        // {
        //     Vector3 diff = lastFramePosition - currFramePosition;
        //     Camera.main.transform.Translate(diff);
        // }


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("PLACING CUBE");

            RaycastHit hitInfo;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }


            // Tile tileUnderMouse = getTileAtMouse(currFramePosition);
            // if (tileUnderMouse != null)
            // {
            //     if (buildingForCreating != null)
            //     {
            //         if (BuildingController.Instance.addBuildingToTile(buildingForCreating, tileUnderMouse))
            //         {
            //             Debug.Log("Building " + buildingForCreating + " Created at " + "(" + tileUnderMouse.X + ", " + tileUnderMouse.Y + ")");
            //         }
            //     }
            // }

        }
        // lastFramePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        Debug.Log("PLACING CUBE");
        var finalPosition = gameGrid.GetNearestPointOnGrid(clickPoint);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }

    public void SetMode_CoalMine()
    {
        buildingForCreating = "Coal Mine";
        setCancelButton();
    }
    public void SetMode_Forest()
    {
        buildingForCreating = "Forest";
        setCancelButton();
    }
    public void SetMode_Hydro()
    {
        buildingForCreating = "Hydro Plant";
        setCancelButton();
    }
    public void SetMode_MovieTheatre()
    {
        buildingForCreating = "Movie Theatre";
        setCancelButton();
    }
    public void SetMode_NationalPark()
    {
        buildingForCreating = "National Park";
        setCancelButton();
    }
    public void SetMode_Nuclear()
    {
        buildingForCreating = "Nuclear Plant";
        setCancelButton();
    }
    public void SetMode_OilRefinery()
    {
        buildingForCreating = "Oil Refinery";
        setCancelButton();
    }
    public void SetMode_RaceTrack()
    {
        buildingForCreating = "Race Track";
        setCancelButton();
    }
    public void SetMode_SolarFarm()
    {
        buildingForCreating = "Solar Farm";
        setCancelButton();
    }
    public void SetMode_WindTurbine()
    {
        buildingForCreating = "Wind Turbine";
        setCancelButton();
    }
    public void SetMode_Zoo()
    {
        buildingForCreating = "Zoo";
        setCancelButton();
    }

    Tile getTileAtMouse(Vector3 coord)
    {
        // ----------------------------------------------------------------------------
        // ----------------------------------------------------------------------------
        // ----------------------------------------------------------------------------
        // MIGHT NEED TO CHANGE TO ROUND INSTEAD OF FLOOR
        return GameController.Instance.Game.getTileAt(Mathf.FloorToInt(coord.x), Mathf.FloorToInt(coord.y));
    }

    private void setCancelButton()
    {
        cancelButtonString.text = "Build Tool:\n" + buildingForCreating + "\nCancel build mode";
    }

    public void setNull()
    {
        buildingForCreating = null;
    }
}