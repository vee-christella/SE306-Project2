using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//Code is from quill18creates youtube Channel, "Unity Base-Building Game Tutorial - Episode 4!"

public class MouseController : MonoBehaviour
{

    // ====================
    public GameObject selectedObject;
    public int red;
    public int green;
    public int blue;
    public bool hoveringOverObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;
    // ====================

    public static MouseController Instance { get; protected set; }

    public GameObject toolTip;

    private TextMeshProUGUI toolTipText;

    private GameGrid gameGrid;
    private Camera mainCamera;
    private string buildingForCreating = null;

    public Button sellButton;

    private TextMeshProUGUI sellText;

    public Text cancelButtonString;
    Vector3 lastFramePosition;

    private float mouseScrollPosition;


    private Tile tileSelected;
    // Start is called before the first frame update
    void Start()
    {
        mouseScrollPosition = Input.GetAxis("Mouse ScrollWheel");
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

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetAxis("Mouse ScrollWheel") != mouseScrollPosition)
        {
            RemoveTooltip();
            mouseScrollPosition = Input.GetAxis("Mouse ScrollWheel");

        }
        if (GameController.Instance.Game.HasStarted)
        {
            RaycastHit hitInfo;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                var gridPosition = gameGrid.GetNearestPointOnGrid(hitInfo.point);

                Tile tileUnderMouse = getTileAtMouse(gridPosition);

                if (tileUnderMouse != null)
                {
                    if (buildingForCreating != null)
                    {
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            // Show a building preview where the user's cursor is on the map
                            BuildingController.Instance.ShowBuildingPreview(buildingForCreating, gameGrid.GetNearestPointOnGrid(hitInfo.point));
                        }

                        // Place a building at the cursor point
                        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                        {
                            if (buildingForCreating != null)
                            {
                                if (BuildingController.Instance.addBuildingToTile(buildingForCreating, tileUnderMouse))
                                {
                                    Debug.Log("Building " + buildingForCreating + " Created at " + "(" + tileUnderMouse.X + ", " + tileUnderMouse.Y + ")");
                                }
                            }

                        }

                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            tileSelected = tileUnderMouse;

                            if (tileUnderMouse.Building != null)
                            {
                                SetToolTip(tileUnderMouse);
                            }
                            else
                            {
                                RemoveTooltip();
                            }
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


    void OnMouseOver()
    {

        selectedObject = GameObject.Find(MouseHoverController.selectedObject);

        // Only highlight buildings
        // if (selectedObject.name.Contains("Building"))
        // {
        Debug.Log("MOUSE OVER");
        hoveringOverObject = true;

        if (!startedFlashing)
        {
            startedFlashing = true;
            StartCoroutine(FlashObject());
        }
        // }

    }

    void OnMouseExit()
    {
        // Only highlight buildings
        // if (selectedObject.name.Contains("Building"))
        // {
        Debug.Log("MOUSE EXIT");
        hoveringOverObject = false;
        startedFlashing = false;
        StopCoroutine(FlashObject());
        selectedObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        // }
    }

    IEnumerator FlashObject()
    {
        while (hoveringOverObject)
        {
            yield return new WaitForSeconds(0.05f);

            if (flashingIn)
            {
                if (blue <= 30)
                {
                    flashingIn = false;
                }
                else
                {
                    blue -= 25;
                    green -= 1;
                }
            }

            if (!flashingIn)
            {
                if (blue >= 250)
                {
                    flashingIn = true;
                }
                else
                {
                    blue += 25;
                    green += 1;
                }
            }
        }
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
        string money, green, happiness;

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

        toolTipText.SetText(name + "\nMoney: " + money + "\nGreen: " + green + "\nHappiness: " + happiness);

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
        sellText.text = "Sell: ";
        sellButton.interactable = false;
        //Debug.Log("Building Deselected");
    }

    public void SetToolTip(Tile tile)
    {
        Building building = tile.Building;
        toolTip.SetActive(true);
        SetToolTipText(tile);

        if (building.Name != "Town Hall")
        {
            sellText.text = "Sell: " + getSellPrice(building);
            sellButton.interactable = true;
        }
    }
}