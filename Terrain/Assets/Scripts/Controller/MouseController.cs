using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//Code is from quill18creates youtube Channel, "Unity Base-Building Game Tutorial - Episode 4!"

public class MouseController : MonoBehaviour
{
    public static MouseController Instance { get; protected set; }

    public GameObject toolTip;

    private TextMeshProUGUI toolTipText;

    private GameGrid gameGrid;
    private Camera mainCamera;
    private string buildingForCreating = null;

    private bool buildingIsSelected = false;

    public Button sellButton;

    private TextMeshProUGUI sellText;

    public Text cancelButtonString;
    Vector3 lastFramePosition;


    private Tile tileSelected;
    // Start is called before the first frame update
    void Start()
    {
        toolTipText = toolTip.GetComponentInChildren<TextMeshProUGUI>();
        sellText = sellButton.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Awake()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Vector3 currFramePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

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

        // try
        // {
        if (GameController.Instance.Game.HasStarted)
        {
            if (buildingForCreating != null)
            {
                RaycastHit hitInfo;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hitInfo))
                {
                    // Show a building preview where the user's cursor is on the map
                    BuildingController.Instance.ShowBuildingPreview(buildingForCreating, gameGrid.GetNearestPointOnGrid(hitInfo.point));
                }

                // Place a building at the cursor point
                if (Input.GetMouseButtonDown(0))
                {
                    // RaycastHit hitInfo;
                    // Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        var gridPosition = gameGrid.GetNearestPointOnGrid(hitInfo.point);

                        Tile tileUnderMouse = getTileAtMouse(gridPosition);

                        if (tileUnderMouse != null)
                        {
                            if (buildingIsSelected)
                            {
                                if (tileUnderMouse.Building != null)
                                {
                                    SetToolTip(tileUnderMouse);
                                }
                                else
                                {
                                    RemoveTooltip();
                                }
                            }
                            else
                            {
                                if (buildingForCreating != null)
                                {
                                    if (BuildingController.Instance.addBuildingToTile(buildingForCreating, tileUnderMouse))
                                    {
                                        Debug.Log("Building " + buildingForCreating + " Created at " + "(" + tileUnderMouse.X + ", " + tileUnderMouse.Y + ")");
                                    }
                                }
                                else if (tileUnderMouse.Building != null)
                                {

                                    buildingIsSelected = true;
                                    SetToolTip(tileUnderMouse);
                                }
                                else if (tileUnderMouse.Building == null)
                                {
                                    RemoveTooltip();
                                }
                            }
                        }
                        else
                        {
                            Debug.Log(".... Tile is null");
                        }
                    }
                }
            }
            else
            {
                
            }
        }
        // }
        // catch
        // {
        //     // Do nothing
        // }
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
        return GameController.Instance.Game.getTileAt((int)coord.x, (int)coord.z);
    }

    private void setCancelButton()
    {
        cancelButtonString.text = "Build Tool:\n" + buildingForCreating + "\nCancel build mode";
    }

    public void setNull()
    {
        buildingForCreating = null;
    }

    private void SetToolTipText(Tile tile)
    {
        Building building = tile.Building;

        //toolTipText.SetText("TestText");
        string name = building.Name;
        string money, green, happiness, sellCost;

        if (tile.IsBuildable(building))
        {
            money = DeltaToString(building.GenerateMoney);
            green = DeltaToString(building.GenerateGreen);
            happiness = DeltaToString(building.GenerateHappiness);
        }
        else
        {
            money = "0";
            green = "0";
            happiness = "0";
        }

        sellCost = getSellPrice(building).ToString();



        toolTipText.SetText(name + "\nMoney: " + money + "\nGreen: " + green + "\nHappiness: " + happiness + "\n\nSell Cost: " + sellCost);

    }

    private string DeltaToString(float delta)
    {
        if (delta >= 0)
        {
            return "+ " + delta.ToString();

        }
        return delta.ToString();


    }

    private double getSellPrice(Building building)
    {
        return building.InitialBuildMoney * 0.25 * -1;
    }

    public void SellBuilding()
    {
        if (tileSelected.Building.Name != "Town Hall")
        {
            GameController.Instance.Game.SellBuilding(tileSelected);
            RemoveTooltip();
        }

    }

    public void RemoveTooltip()
    {
        toolTip.SetActive(false);
        buildingIsSelected = false;
        sellText.text = "Sell: ";
        sellButton.interactable = false;
        //Debug.Log("Building Deselected");
    }

    public void SetToolTip(Tile tile)
    {
        Building building = tile.Building;
        toolTip.SetActive(true);
        toolTip.transform.position = Input.mousePosition;
        SetToolTipText(tile);

        if (building.Name != "Town Hall")
        {
            sellText.text = "Sell: " + getSellPrice(building);
            sellButton.interactable = true;
        }
    }
}