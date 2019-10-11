using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//Code is from quill18creates youtube Channel, "Unity Base-Building Game Tutorial - Episode 4!"
public class MouseController : MonoBehaviour
{

    public GameObject toolTip;

    private TextMeshProUGUI toolTipText;

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

    // Update is called once per frame
    void Update()
    {
        Vector3 currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //If mouse over a UI element, bail out
        if ( EventSystem.current.IsPointerOverGameObject() ){
            return;
        }


        if (Input.GetMouseButton(1))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);

            //Remove tooltip
            toolTip.SetActive(false);
            buildingIsSelected = false;
            sellText.text = "Sell: ";
            sellButton.interactable = false;
            Debug.Log("Building Deselected");
        }
        if (Input.GetMouseButtonUp(0))
        {
            Tile tileUnderMouse = getTileAtMouse(currFramePosition);
            tileSelected = getTileAtMouse(currFramePosition);
            if (tileUnderMouse != null)
            {
                if (buildingIsSelected) 
                {
                    if (tileUnderMouse.Building != null)
                    {
                        buildingIsSelected = true;
                        Debug.Log("Building Selected");
                        //Show tooltip
                        toolTip.SetActive(true);
                        toolTip.transform.position = Input.mousePosition;
                        SetToolTipText(tileUnderMouse.Building);
                        sellText.text = "Sell: " + getSellPrice(tileUnderMouse.Building);
                        sellButton.interactable = true;
                        
                    }
                    else
                    {
                        //Remove tooltip
                        toolTip.SetActive(false);
                        buildingIsSelected = false;
                        sellText.text = "Sell: ";
                        sellButton.interactable = false;
                        Debug.Log("Building Deselected");
                    }
             
                }
                else
                {
                    if (buildingForCreating != null) //Building has been selected to build
                    {
                        if (BuildingController.Instance.addBuildingToTile(buildingForCreating, tileUnderMouse))
                        {

                            Debug.Log("Building " + buildingForCreating + " Created at " + "(" + tileUnderMouse.X + ", " + tileUnderMouse.Y + ")");
                        }
                    }
                    else if (tileUnderMouse.Building != null)
                    {
                        buildingIsSelected = true;
                        Debug.Log("Building Selected");
                        toolTip.SetActive(true);                 
                        toolTip.transform.position = Input.mousePosition;
                        SetToolTipText(tileUnderMouse.Building);
                        sellText.text = "Sell: " + getSellPrice(tileUnderMouse.Building);
                        sellButton.interactable = true;



                    }
                }
            }

        }
        lastFramePosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetMode_CoalMine() {
        buildingForCreating = "Coal Mine";
        setCancelButton();
    }
    public void SetMode_Forest() {
        buildingForCreating = "Forest";
        setCancelButton();
    }
    public void SetMode_Hydro() {
        buildingForCreating = "Hydro Plant";
        setCancelButton();
    }
    public void SetMode_MovieTheatre() {
        buildingForCreating = "Movie Theatre";
        setCancelButton();
    }
    public void SetMode_NationalPark() {
        buildingForCreating = "National Park";
        setCancelButton();
    }
    public void SetMode_Nuclear() {
        buildingForCreating = "Nuclear Plant";
        setCancelButton();
    }
    public void SetMode_OilRefinery() {
        buildingForCreating = "Oil Refinery";
        setCancelButton();
    }
    public void SetMode_RaceTrack() {
        buildingForCreating = "Race Track";
        setCancelButton();
    }
    public void SetMode_SolarFarm(){
        buildingForCreating = "Solar Farm";
        setCancelButton();
    }
    public void SetMode_WindTurbine() {
        buildingForCreating = "Wind Turbine";
        setCancelButton();
    }
    public void SetMode_Zoo() {
        buildingForCreating = "Zoo";
        setCancelButton();
    }

    Tile getTileAtMouse(Vector3 coord)
    {
        return GameController.Instance.Game.getTileAt(Mathf.FloorToInt(coord.x), Mathf.FloorToInt(coord.y));
    }

    private void setCancelButton(){
        cancelButtonString.text = "Build Tool:\n" + buildingForCreating + "\nCancel build mode";
    }

    public void setNull(){
        buildingForCreating = null;
    }

    private void SetToolTipText(Building building)
    {

        //toolTipText.SetText("TestText");
        string name = building.Name;
        string money, green, happiness, sellCost;

        money = DeltaToString(building.GenerateMoney);
        green = DeltaToString(building.GenerateGreen);
        happiness = DeltaToString(building.GenerateHappiness);
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
        GameController.Instance.Game.SellBuilding(tileSelected);
    }
}