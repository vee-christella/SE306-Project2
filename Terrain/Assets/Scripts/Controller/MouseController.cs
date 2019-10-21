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
    private GameGrid gameGrid;
    private Camera mainCamera;
    Vector3 lastFramePosition;
    private float mouseScrollPosition;
    public GameObject toolTip;
    public Text cancelButtonString;
    public TextMeshProUGUI toolTipCoin;
    public TextMeshProUGUI toolTipGreen;
    public TextMeshProUGUI toolTipHappiness;
    public TextMeshProUGUI toolTipName;


    private TextMeshProUGUI sellText;
    public Button sellButton;
    public AudioSource CantBuildSound;
    public AudioClip CantBuildClip;

    public AudioSource BuildSound;
    public AudioClip BuildClip;


    private Tile tileSelected;
    private string buildingForCreating = null;

    void Start()
    {
        mouseScrollPosition = Input.GetAxis("Mouse ScrollWheel");
        sellText = sellButton.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Awake()
    {
        Instance = this;
        gameGrid = FindObjectOfType<GameGrid>();
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {

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
                                    BuildSound.PlayOneShot(BuildClip);
                                    Debug.Log("Building " + buildingForCreating + " Created at " + "(" + tileUnderMouse.X + ", " + tileUnderMouse.Y + ")");
                                }
                                else
                                {
                                    //Play cant build music
                                    CantBuildSound.PlayOneShot(CantBuildClip);

                                }
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
                // Remove the building preview when the cursor is not on a tile
                BuildingController.Instance.HideBuildingPreview();
            }
        }
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

    /*
    All of the SetMode_X() methods are used by the UI shop buttons. They tell the code which building
    the player currently has selected from the shop.

    =================================================================================================
    */

    public void SetMode_AnimalFarm()
    {
        buildingForCreating = "Animal Farm";
        setCancelButton();
    }
    public void SetMode_BeeFarm()
    {
        buildingForCreating = "Bee Farm";
        setCancelButton();
    }
    public void SetMode_CoalMine()
    {
        buildingForCreating = "Coal Mine";
        setCancelButton();
    }
    public void SetMode_Factory()
    {
        buildingForCreating = "Factory";
        setCancelButton();
    }
    public void SetMode_Forest()
    {
        buildingForCreating = "Forest";
        setCancelButton();
    }
    public void SetMode_Greenhouse()
    {
        buildingForCreating = "Greenhouse";
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
    public void SetMode_Pollutant()
    {
        buildingForCreating = "Pollutant";
        setCancelButton();
    }
    public void SetMode_RaceTrack()
    {
        buildingForCreating = "Race Track";
        setCancelButton();
    }
    public void SetMode_RecyclingPlant()
    {
        buildingForCreating = "Reycling Plant";
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
    public void SetMode_VegetableFarm()
    {
        buildingForCreating = "Vegetable Farm";
        setCancelButton();
    }
    public void SetMode_Zoo()
    {
        buildingForCreating = "Zoo";
        setCancelButton();
    }

    /*

    */
    private Tile getTileAtMouse(Vector3 coord)
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

        toolTipName.SetText(name);
        toolTipCoin.SetText(money);
        toolTipGreen.SetText(green);
        toolTipHappiness.SetText(happiness);


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
        double modifier;

        switch (PlayerPrefs.GetInt("Level"))
        {
            case 1:
                modifier = 0.25;
                break;
            case 2:
                modifier = 0.2;
                break;
            case 3:
                modifier = 0.1;
                break;
            default:
                modifier = 0.1;
                break;
        }

        return building.InitialBuildMoney * modifier * -1;//
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