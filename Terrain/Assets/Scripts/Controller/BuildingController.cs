using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private Dictionary<string, GameObject> modelDictionary = new Dictionary<string, GameObject>();
    public GameObject model_AnimalFarm;
    public GameObject model_BeeFarm;
    public GameObject model_CoalMine;
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

    public static BuildingController Instance { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        // Add models to dictionary for easy access when building
        modelDictionary.Add("AnimalFarm", model_AnimalFarm);
        modelDictionary.Add("BeeFarm", model_BeeFarm);
        modelDictionary.Add("CoalMine", model_CoalMine);
        modelDictionary.Add("Factory", model_Factory);
        modelDictionary.Add("Forest", model_Forest);
        modelDictionary.Add("Greenhouse", model_Greenhouse);
        modelDictionary.Add("Hydro", model_Hydro);
        modelDictionary.Add("MovieTheatre", model_MovieTheatre);
        modelDictionary.Add("NationalPark", model_NationalPark);
        modelDictionary.Add("Nuclear", model_Nuclear);
        modelDictionary.Add("OilRefinery", model_OilRefinery);
        modelDictionary.Add("RaceTrack", model_RaceTrack);
        modelDictionary.Add("RecyclingPlant", model_RecyclingPlant);
        modelDictionary.Add("SolarFarm", model_SolarFarm);
        modelDictionary.Add("TownHall", model_TownHall);
        modelDictionary.Add("VegetableFarm", model_VegetableFarm);
        modelDictionary.Add("WindTurbine", model_WindTurbine);
        modelDictionary.Add("Zoo", model_Zoo);
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
        Debug.Log("CHANGE BUILDING SPRITE");
        tile.unregisterMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingSprite(tileBuildingData, buildingGO); });
        GameObject newBuilding = PlaceCubeNear(tile, buildingGO);
        tile.registerMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingSprite(tileBuildingData, newBuilding); });
    }


    private GameObject PlaceCubeNear(Tile tile, GameObject building)
    {
        Debug.Log("_______OBJECT: " + building);
        Debug.Log("_______BUILDING CLASS: " + tile.Building.GetType().Name);

        string newBuildingModel = tile.Building.GetType().Name;

        Debug.Log("_______NEW OBJECT: " + modelDictionary[tile.Building.GetType().Name]);

        // Create new building
        GameObject newBuilding = Instantiate(modelDictionary[tile.Building.GetType().Name]);
        newBuilding.name = building.name;
        newBuilding.transform.position = building.transform.position;

        // Delete old building
        Destroy(building);

        return newBuilding;
    }
}
