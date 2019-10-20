using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    public enum BuildingType
    {
        Utility,
        Recreational,
        EnergySource,
        Misc
    };

    float initialBuildMoney;
    float initialBuildGreen;
    float initialBuildHappiness;
    float generateMoney;
    float generateGreen;
    float generateHappiness;
    int id = 0;
    int turnsToBuild;
    BuildingType typeOfBuilding;
    string name;
    string blurb;

    public float InitialBuildMoney { get => initialBuildMoney; set => initialBuildMoney = value; }
    public float InitialBuildGreen { get => initialBuildGreen; set => initialBuildGreen = value; }
    public float InitialBuildHappiness { get => initialBuildHappiness; set => initialBuildHappiness = value; }
    public float GenerateMoney { get => generateMoney; set => generateMoney = value; }
    public float GenerateGreen { get => generateGreen; set => generateGreen = value; }
    public float GenerateHappiness { get => generateHappiness; set => generateHappiness = value; }
    public int Id { get => id; set => id = value; }
    public int TurnsToBuild { get => turnsToBuild; set => turnsToBuild = value; }
    public BuildingType TypeOfBuilding { get => typeOfBuilding; set => typeOfBuilding = value; }
    public string Name { get => name; set => name = value; }
    public string Blurb { get => blurb; set => blurb = value; }


    // Constructor to initialise the building with their respective stats
    public Building(float initBuildMoney, float initBuildGreen,
    float initBuildHappiness, float genMoney, float genGreen)
    {
        this.initialBuildMoney = initBuildMoney;
        this.initialBuildGreen = initBuildGreen;
        this.initialBuildHappiness = initBuildHappiness;

        this.generateGreen = genGreen;
        this.generateMoney = genMoney;
    }

    public Building(float initBuildMoney, float initBuildGreen,
    float initBuildHappiness, float genMoney, float genGreen, float genHappiness,
        int buildTime)
    {
        this.initialBuildMoney = initBuildMoney;
        this.initialBuildGreen = initBuildGreen;
        this.initialBuildHappiness = initBuildHappiness;

        this.generateGreen = genGreen;
        this.generateMoney = genMoney;
        this.generateHappiness = genHappiness;
        this.turnsToBuild = buildTime;
    }

    public Building(int id, float initBuildMoney, float initBuildGreen,
    float initBuildHappiness, float genMoney, float genGreen, float genHappiness,
        int buildTime)
    {
        Id = id;
        this.initialBuildMoney = initBuildMoney;
        this.initialBuildGreen = initBuildGreen;
        this.initialBuildHappiness = initBuildHappiness;

        this.generateGreen = genGreen;
        this.generateMoney = genMoney;
        this.generateHappiness = genHappiness;
        this.turnsToBuild = buildTime;

    }

    /*
    Gets the class name of a building based on the "name" of the building
    */
    public static string resolveBuildingClassName(string s)
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

    /* This method is called each time a building is to be built on a tile. 
    Some builds are restricted to the tile that it is being on. For example,
    no buildings can be built if the tile is of mountain type. */
    // public bool IsBuildable(Tile tile)
    // {

    //     // No buildings can be built on mountain tiles
    //     if (tile.Type == Tile.TileType.Mountain)
    //     {
    //         return false;
    //     }

    //     // Hydro plants can only be built on water tiles
    //     else if (this.name.Equals("Hydro Plant"))
    //     {
    //         if (tile.Type != Tile.TileType.Water)
    //         {
    //             return false;
    //         }

    //         // Oil Refineries can only be built on desert tiles
    //     } else if (this.name.Equals("Oil Refinery"))
    //     {
    //         if (tile.Type != Tile.TileType.Desert)
    //         {
    //             return false;
    //         }

    //         // Forests can only be built on grassland tiles
    //     } else if (this.name.Equals("Forest"))
    //     {
    //         if (tile.Type != Tile.TileType.Plain)
    //         {
    //             return false;
    //         }

    //         //Zoos can only be built on plains
    //     } else if (this.name.Equals("Zoo")){
    //         if (tile.Type != Tile.TileType.Plain){
    //             return false;
    //         }
    //     }

    //     return true;
    // }

}



