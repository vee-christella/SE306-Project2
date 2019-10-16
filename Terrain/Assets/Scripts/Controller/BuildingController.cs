using System;
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
    private GameObject previewBuilding;

    public static BuildingController Instance { get; protected set; }

    // Start is called before the first frame update

    void Awake()
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
    void Start()
    {


    }

    public bool addBuildingToTile(string buildingType, Tile tile)
    {
        Building building = GameController.Instance.Game.addBuildingToTile(buildingType, tile);
        return (building != null);
    }

    public void ChangeBuildingModel(Tile tile, GameObject buildingGO)
    {
        // Remove the old building's callback method
        Debug.Log("callback 3");
        tile.unregisterMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingModel(tileBuildingData, buildingGO); });
        Debug.Log("callback 4");
        GameObject newBuilding = PlaceCubeNear(tile, buildingGO);
        Debug.Log("callback 5");
        // Add the new building's and callback method
        tile.registerMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingModel(tileBuildingData, newBuilding); });
        Debug.Log("callback 6");
    }


    private GameObject PlaceCubeNear(Tile tile, GameObject building)
    {
        GameObject newBuilding;
        Debug.Log("callback 7");
        if (tile.Building != null)
        {
            // Get the class name of the building
            string newBuildingModel = tile.Building.GetType().Name;

            // Create new building GameObject
            newBuilding = Instantiate(modelDictionary[tile.Building.GetType().Name]);
        }
        else
        {
            newBuilding = new GameObject();
        }
        Debug.Log("callback 8");
        newBuilding.name = "Building(" + tile.X + ", " + tile.Y + ", " + tile.Z + ")";
        newBuilding.transform.position = new Vector3(tile.X, tile.Y, tile.Z);
        Debug.Log("callback placecubwear called");
        // Delete old (possibly empty) building GameObject
        Destroy(building);

        return newBuilding;
    }

    public void ShowBuildingPreview(string name, Vector3 mousePoint)
    {
        // Remove the preview building from where the cursor previously was
        Destroy(previewBuilding);

        string buildingName = resolveBuildingName(name);

        previewBuilding = Instantiate(modelDictionary[buildingName]);
        previewBuilding.name = "PreviewBuilding";

        // Remove the preview object's box collider to prevent a hover changing its colour
        Destroy(previewBuilding.GetComponent<Collider>());

        // Show the preview building on the cursor point
        previewBuilding.transform.position = mousePoint;

        if (canBuildOnPoint(buildingName, mousePoint))
        {
            // Set the preview building to a green colour
            previewBuilding.GetComponent<Renderer>().material.color = new Color32(0, 200, 0, 100);
        }
        else
        {
            // Set the preview building to a red colour
            previewBuilding.GetComponent<Renderer>().material.color = new Color32(200, 0, 0, 100);
        }
    }

    /*
    Gets the class name of a building based on the "name" of the building
    */
    private string resolveBuildingName(string s)
    {
        if (s == "Animal Farm") return "AnimalFarm";
        if (s == "Bee Farm") return "BeeFarm";
        if (s == "Coal Mine") return "CoalMine";
        if (s == "Forest") return "Forest";
        if (s == "Hydro Plant") return "Hydro";
        if (s == "Movie Theatre") return "MovieTheatre";
        if (s == "National park") return "NationalPark";
        if (s == "Nuclear Plant") return "Nuclear";
        if (s == "Oil Refinery") return "OilRefinery";
        if (s == "Race Track") return "RaceTrack";
        if (s == "Recycling Plant") return "RecyclingPlant";
        if (s == "Solar Farm") return "SolarFarm";
        if (s == "Town Hall") return "TownHall";
        if (s == "Vegetable Farm") return "VegetableFarm";
        if (s == "Wind Turbine") return "WindTurbine";
        if (s == "Zoo") return "Zoo";

        // Should never return null if MouseController's SetMode_x() methods are implemented correctly
        return null;
    }

    /*
    Checks whether the building can be built on the point
    */
    private bool canBuildOnPoint(string buildingName, Vector3 point)
    {
        // Create a temporary building and check if it can be built on the tile at the point
        Building building = (Building)Activator.CreateInstance(Type.GetType(buildingName));
        Tile tile = GameController.Instance.Game.getTileAt((int)point.x, (int)point.z);

        if (tile.IsBuildable(building)) return true;

        return false;
    }



}
