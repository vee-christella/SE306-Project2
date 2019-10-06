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
    private Event gameEvent;

    float currentTurn;
    float maxTurns;
    float maxGreen;
    int turn;

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
    public int Turn { get => turn; set => turn = value; }
    public Event GameEvent { get => gameEvent; set => gameEvent = value; }

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

    /* This method proceeds with the next turn after the user clicks the 
     * end turn button. It increments the accumulated points and shows it on 
     * the metrics
     */
    public void nextTurn()
    {
        GameEvent = null;
        this.currentTurn++;

        GameEvent = EventForNextTurn();

        if (GameEvent != null)
        {
          //  GenerateMoney = GenerateMoney + GameEvent.MoneyDelta;
           // GenerateGreen = GenerateGreen + GameEvent.GreenPointDelta;
           // GenerateHappiness = GenerateHappiness + GameEvent.HappinessDelta;
        }

        // Increase the metrics
        this.money = Money + GenerateMoney;
        this.green = Green + GenerateGreen;
        this.happiness = Happiness + GenerateHappiness;

        //// Display updated metrics
        //metricsCont.SetMetrics(money, green, happiness);
        //metricsCont.SetTurn(currentTurn);

        // Check if the user has won the game by reaching the number of green
        // points required
        if (this.green >= maxGreen)
        {
            this.endGame(true);

            // Check if the user has lost the game by exceeding the max number
            // of turns allowed 
        }
        else if (currentTurn >= maxTurns)
        {
            this.endGame(false);
        }
        else
        {

            // TODO: Method for user actions
        }

    }

    public Event EventForNextTurn()
    {
        List<Event> randomEventList = InitaliseRandomEventList();
        Random random = new Random();
        if (currentTurn == 2)
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

    public List<Event> InitaliseRandomEventList()
    {
        List<Event> randomEventList = new List<Event>();

        randomEventList.Add(new AcidRain());
        randomEventList.Add(new Earthquake());
        randomEventList.Add(new ForestSpawn());
        randomEventList.Add(new Tsunami());

        return randomEventList;
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
            case "Hydro":
                building = new Hydro();
                break;
            case "CoalMine":
                building = new CoalMine();
                break;
            case "Zoo":
                building = new Zoo();
                break;
            case "WindTurbine":
                building = new WindTurbine();
                break;
            case "SolarFarm":
                building = new SolarFarm();
                break;
            case "RaceTrack":
                building = new RaceTrack();
                break;
            case "OilRefinery":
                building = new OilRefinery();
                break;
            case "Nuclear":
                building = new Nuclear();
                break;
            case "NationalPark":
                building = new NationalPark();
                break;
            case "MovieTheatre":
                building = new MovieTheatre();
                break;
            case "Forest":
                building = new Forest();
                break;
            default:
                return null;
        }
        if (tile.placeBuilding(building))
        {
            buildings[tile.X, tile.Y] = building;
            return building;
        }
        else
        {
            return null;
        }

    }
}
