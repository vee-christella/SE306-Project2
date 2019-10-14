using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public GameObject model_AnimalFarm;
    public GameObject model_BeeFarmModel;
    public GameObject model_CoalMineModel;
    public GameObject model_Factory;
    public GameObject model_Forest;
    public GameObject model_Greenhouse;
    public GameObject model_Hydro;
    public GameObject model_MovieTheatre;
    public GameObject model_NationalPark;
    public GameObject model_Nuclear;
    public GameObject model_OilRefinery;
    public GameObject model_RaceTrack;
    public GameObject model_RecyclingPlant;
    public GameObject model_SolarFarm;
    public GameObject model_TownHall;
    public GameObject model_VegetableFarm;
    public GameObject model_WindTurbine;
    public GameObject model_Zoo;



    public GameObject cube;




    public static BuildingController Instance { get; protected set; }

    public Sprite[] buildingSprites = new Sprite[12];
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool addBuildingToTile(string buildingType, Tile tile)
    {
        Building building = GameController.Instance.Game.addBuildingToTile(buildingType, tile);



        if (building != null)
        {
            Debug.Log("==== BuildingController building not null");

            //    GameObject buildingGO = new GameObject();
            //     buildingGO.name = "Building(" + tile.X + ", " + tile.Y + ")";
            //     buildingGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);
            //    SpriteRenderer buildingSR = buildingGO.AddComponent<SpriteRenderer>();
            //     buildingSR.sortingLayerName="Building";
            //    buildingSR.sprite = buildingSprites[building.Id];
            return true;
        }
        else
        {
            Debug.Log("==== BuildingController building is null");
            return false;
        }
    }

    public void ChangeBuildingSprite(Tile tile, GameObject buildingGO)
    {
        // buildingGO.GetComponent<SpriteRenderer>().sprite = buildingSprites[tile.Building.Id];
        Debug.Log("CHANGE BUILDING SPRITE");
        tile.unregisterMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingSprite(tileBuildingData, buildingGO); });
        GameObject newBuilding = PlaceCubeNear(tile, buildingGO);
        tile.registerMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingSprite(tileBuildingData, newBuilding); });
    }


    private GameObject PlaceCubeNear(Tile tile, GameObject building)
    {
        Debug.Log("PLACING CUBE");

        string buildingName = "Building(" + building.transform.position.x + ", " + building.transform.position.y + ", " + building.transform.position.z + ")";
        Debug.Log("_______NAME: " + buildingName);
        Debug.Log("_______OBJECT: " + building);

        // Create new building
        GameObject newBuilding = Instantiate(cube);
        newBuilding.name = building.name;
        newBuilding.transform.position = building.transform.position;

        // Delete old building
        Destroy(building);

        return newBuilding;
    }
}
