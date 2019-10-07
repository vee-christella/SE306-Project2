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
    Event gameEvent;
    float currentTurn;
    float maxTurns;
    float maxGreen;
    bool isEnd;
    bool isVictory;

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
    public bool IsEnd { get => isEnd; set => isEnd = value; }
    public bool IsVictory { get => isVictory; set => isVictory = value; }
    public Event GameEvent { get => gameEvent; set => gameEvent = value; }

    public Game(int rows = 30, int columns = 30)
    {
        this.isEnd = false;
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

    public void InitialiseMetrics(float money, float green, float happiness, float maxGreen)
    {
        Money = money;
        Green = green;
        Happiness = happiness;
        MaxGreen = maxGreen;
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

        // Check if the user has won the game by reaching the number of green
        // points required
        if (this.green >= maxGreen)
        {
            this.endGame(true);
            // Check if the user has lost the game by exceeding the max number
            // of turns allowed, or having a negative money value (as they
            // now are stuck in debt)

            return;
        }
        else if (currentTurn >= maxTurns || Money < 0)
        {
            this.endGame(false);
            return;
        }

        GameEvent = EventForNextTurn();

        if (GameEvent != null)
        {
            GenerateMoney = GenerateMoney + GameEvent.MoneyDelta;
            GenerateHappiness = GenerateHappiness + GameEvent.HappinessDelta;
            GenerateGreen = GenerateGreen + GameEvent.GreenPointDelta;
        }

        // Increase the metrics
        Money = Money + GenerateMoney;
        Green = Green + GenerateGreen;
        Happiness = Happiness + GenerateHappiness;
    }

    public void endGame(bool isVictory)
    {
        this.isEnd = true;
        this.IsVictory = isVictory;
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


    // get the event  for the next turn
    public Event EventForNextTurn()
    {
        List<Event> randomEventList = InitaliseRandomEventList();
        Random random = new Random();
        if (currentTurn == 5)
        {
            Debug.Log("turn 2");
            return new Drought();
        }
        else if (Random.Range(0, 100) < 10)
        {
            return randomEventList[Random.Range(0, randomEventList.Count)];
        }

        return null;
    }


    // method to create list of all random events
    public List<Event> InitaliseRandomEventList()
    {
        List<Event> randomEventList = new List<Event>();

        randomEventList.Add(new AcidRain());
        randomEventList.Add(new Earthquake());
        randomEventList.Add(new ForestSpawn());
        randomEventList.Add(new Tsunami());

        return randomEventList;
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
