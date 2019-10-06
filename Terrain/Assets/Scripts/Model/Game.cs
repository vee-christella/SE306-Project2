using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    int rows;
    int columns;
    Tile[,] tiles;
    float money;
    float green;
    float happiness;
    float generateMoney;
    float generateGreen;
    float generateHappiness;
    Building[,] buildings;
    float currentTurn;
    float maxTurns;
    float maxGreen;

    public int Rows { get => rows; }
    public int Columns { get => columns; }
    public float Money { get => money; set => money = value; }
    public float Green { get => green; set => green = value; }
    public float Happiness { get => happiness; set => happiness = value; }
    public float GenerateMoney { get => generateMoney; set => generateMoney = value; }
    public float GenerateGreen { get => generateGreen; set => generateGreen = value; }
    public float GenerateHappiness { get => generateHappiness; set => generateHappiness = value; }
    public float CurrentTurn { get => currentTurn; set => currentTurn = value; }
    public float MaxTurns { get => maxTurns; set => maxTurns = value; }
    public float MaxGreen { get => maxGreen; set => maxGreen = value; }

    public Game(int rows = 30, int columns = 30)
    {
        this.currentTurn = 0;
        this.rows = rows;
        this.columns = columns;
        tiles = new Tile[rows, columns];
        buildings = new Building[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                tiles[i, j] = new Tile(this, i, j);
            }
        }
        Debug.Log("game created");



    }


    public Tile getTileAt(int x, int y)
    {
        if (x >= rows || x < 0 || y >= columns || y < 0)
        {
            return null;
        }
        return tiles[x, y];
    }

    public void InitialiseMetrics(float money, float green, float happiness)
    {
        Money = money;
        Green = green;
        Happiness = happiness;
    }

    public void InitialiseTurns(float currentTurn, float maxTurn)
    {
        CurrentTurn = currentTurn;
        MaxTurns = maxTurn;
    }

    /* This method proceeds with the next turn after the user clicks the 
     * end turn button. It increments the accumulated points and shows it on 
     * the metrics
     */
    public void nextTurn()
    {
        this.currentTurn++;

        // Increase the metrics
        Money = Money + GenerateMoney;
        Green = Green + GenerateGreen;
        Happiness = Happiness + GenerateHappiness;
        // Check if the user has won the game by reaching the number of green
        // points required
        if (this.green >= maxGreen)
        {
            this.endGame(true);
            // Check if the user has lost the game by exceeding the max number
            // of turns allowed, or having a negative money value (as they
            // now are stuck in debt)
        }

        else if (currentTurn >= maxTurns || Money < 0)
        {
            this.endGame(false);
        }
    }

    public void endGame(bool isVictory)
    {
        // TODO: Victory/Fail screen goes here
    }

    public Building addBuildingToTile(string buildingType, Tile tile)
    {
        Building building = null;
        switch (buildingType)
        {
            case "Hydro Plant":
                building = new Hydro();
                break;
            case "Coal Mine":
                building = new CoalMine();
                break;
            case "Zoo":
                building = new Zoo();
                break;
            case "Wind Turbine":
                building = new WindTurbine();
                break;
            case "Solar Farm":
                building = new SolarFarm();
                break;
            case "Race Track":
                building = new RaceTrack();
                break;
            case "Oil Refinery":
                building = new OilRefinery();
                break;
            case "Nuclear Plant":
                building = new Nuclear();
                break;
            case "National Park":
                building = new NationalPark();
                break;
            case "Movie Theatre":
                building = new MovieTheatre();
                break;
            case "Forest":
                building = new Forest();
                break;
            default:
                return null;
        }


        // Check if funds are sufficient
        if (Money + building.InitialBuildMoney >= 0)
        {
            if (tile.placeBuilding(building))
            {
                buildings[tile.X, tile.Y] = building;
                UpdateMetrics(building);
                return building;
            }
            else
            {
                // TODO: display pop up to say tile is unavailable to be built
                return null;
            }
        }
        else
        {
            // TODO: display pop up to say "INSUFFICIENT FUNDS"
            return null;

        }


    }


    // Change the metrics with regards to the effects of the building
    // that has just been placed.
    public void UpdateMetrics(Building building)
    {
        Money += building.InitialBuildMoney;
        Green += building.InitialBuildGreen;

        if (Happiness + building.InitialBuildHappiness < 0)
        {
            Happiness = 0;
        }
        else if (Happiness + building.InitialBuildHappiness > 100)
        {
            Happiness = 100;
        }
        else
        {
            Happiness += building.InitialBuildHappiness;
        }

        GenerateMoney += building.GenerateMoney;
        GenerateGreen += building.GenerateGreen;
        GenerateHappiness += building.GenerateHappiness;

        GameController.Instance.SetMetrics(Money, Green, Happiness);


    }
}
