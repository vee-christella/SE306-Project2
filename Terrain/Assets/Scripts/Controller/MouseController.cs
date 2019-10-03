using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code is from quill18creates youtube Channel, "Unity Base-Building Game Tutorial - Episode 4!"
public class MouseController : MonoBehaviour
{

    string buildingForCreating = null;

    Vector3 lastFramePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(1))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Tile tileUnderMouse = getTileAtMouse(currFramePosition);
            if (tileUnderMouse != null)
            {
                if (buildingForCreating != null)
                {
                    if(BuildingController.Instance.addBuildingToTile(buildingForCreating, tileUnderMouse))
                    {

                        Debug.Log("Building "+ buildingForCreating+ " Created at " +"(" + tileUnderMouse.X + ", " + tileUnderMouse.Y + ")");
                    }
                }
            }

        }
        lastFramePosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetMode_CoalMine() {
        buildingForCreating = "CoalMine";
    }
    public void SetMode_Forest() {
        buildingForCreating = "Forest";
    }
    public void SetMode_Hydro() {
        buildingForCreating = "Hydro";
    }
    public void SetMode_MovieTheatre() {
        buildingForCreating = "MovieTheatre";
    }
    public void SetMode_NationalPark() {
        buildingForCreating = "NationalPark";
    }
    public void SetMode_Nuclear() {
        buildingForCreating = "Nuclear";
    }
    public void SetMode_OilRefinery() {
        buildingForCreating = "OilRefinery";
    }
    public void SetMode_RaceTrack() {
        buildingForCreating = "RaceTrack";
    }
    public void SetMode_SolarFarm(){
        buildingForCreating = "SolarFarm";
    }
    public void SetMode_WindTurbine() {
        buildingForCreating = "WindTurbine";
    }
    public void SetMode_Zoo() {
        buildingForCreating = "Zoo";
    }

    Tile getTileAtMouse(Vector3 coord)
    {
        return GameController.Instance.Game.getTileAt(Mathf.FloorToInt(coord.x), Mathf.FloorToInt(coord.y));
    }
}