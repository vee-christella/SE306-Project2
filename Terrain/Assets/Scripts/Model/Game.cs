using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif



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
    GameDifficulty gameDifficulty;
    Event gameEvent;
    float currentTurn;
    float maxTurns;
    float maxGreen;
    bool isEnd = false;
    bool isVictory;
    List<Event> goodEventList = new List<Event>();
    List<Event> badEventList = new List<Event>();


    GameObject errorMessage;


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
    public Tile[,] Tiles { get => tiles; set => tiles = value; }

    public enum GameDifficulty
    {
        Easy, Medium, Hard
    };

    public Game(int rows = 30, int columns = 30)
    {
        this.isEnd = false;
        this.currentTurn = 0;
        this.rows = rows;
        this.columns = columns;
        
        Tiles = new Tile[rows, columns];
        buildings = new Building[rows, columns];

        InitaliseRandomEventLists();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Tiles[i, j] = new Tile(this, i, j);
                Tiles[i, j].registerMethodCallbackTypeChanged(StillBuildable);
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
        return Tiles[x, y];
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
    public void NextTurn()
    {
        this.currentTurn++;
        Debug.Log("turn " + this.currentTurn);

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

            return;
        }
        else if (currentTurn >= maxTurns || Money < 0)
        {
            this.endGame(false);
            return;
        }

        GameEvent = EventForNextTurn();

        if (GameEvent != null && GameEvent.GetType().Name.ToString() == "RisingSeaLevel")
        {
            badEventList.Remove(GameEvent);
        }


        if (GameEvent != null)
        {
            Money = Money + GameEvent.MoneyDelta;
            Happiness = Happiness + GameEvent.HappinessDelta;
            Green = Green + GameEvent.GreenPointDelta;
        }


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
            case "Town Hall":
                building = new TownHall();
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
                if (tile.Building != null)
                {
                    GameController.Instance.ShowError("Another building already exists on this tile.");
                }
                else
                {
                    // Show error message
                    GameController.Instance.ShowError(building.Name + " cannot be built on a " + tile.Type + " tile.");
                }

                return null;
            }
        }
        else
        {

            // Show error message
            GameController.Instance.ShowError("You do not have enough money to build a " + building.Name + ". ");

            return null;

        }


    }

    public void SellBuilding(Tile tile)
    {
        Debug.Log("Yeet");
        Building building = tile.Building;
        float CostToSell = building.InitialBuildMoney * (float)0.25 * -1;
        if (tile.removeBuilding())
        {
            buildings[tile.X, tile.Y] = null;
            Money += CostToSell;
            GameController.Instance.SetMetrics(Money, Green, Happiness);

        }
    }


    // get the event  for the next turn
    public Event EventForNextTurn()
    {
        // current turn increase increases probability
        // green point decreases per turn probability
        // game difficultly increases or decreases probability

        List<Event> potentionalEvents = new List<Event>();
        float badEventProbability = 0;
        float goodEventProbability;
        float difficultyOffset = 0.1f;


 //       switch (gameDifficulty)
 //       {
 //           case GameDifficulty.Easy:
 //               difficultyOffset = 0.1f;
 //               break;
 //           case GameDifficulty.Medium:
//                difficultyOffset = 0.2f;
 //               break;
//            case GameDifficulty.Hard:
 //               difficultyOffset = 0.3f;
 //               break;
 //           default:
 //               difficultyOffset = 0.1f;
 //               break;
 //       }


        goodEventProbability = (green / 2000) * 100;

        // check green points to account for negative value
        if (green < 0)
        {
            badEventProbability = (1 - 700 / (1000 - green));
        }
        else
        {
            badEventProbability = (300 / (1000 + green));
        }

        // calculate probability so no. of random events increases as progress through the game increases
        // max probability of random events occuring is 80%
        badEventProbability = Mathf.FloorToInt(((difficultyOffset + (0.7f * badEventProbability)) * (currentTurn / maxTurns)) * 100);

        if (badEventProbability > 100)
        {
            goodEventProbability = 0;
            badEventProbability = 80;
        }

        int randomNum = Random.Range(1, 101);

        // good events take priority and the chance of a good event increases the more green points the user has
        if (randomNum < goodEventProbability)
        {

            potentionalEvents.Add(goodEventList[Random.Range(0, goodEventList.Count)]);
            // return random good event 
        }
        else
        {
            randomNum = Random.Range(1, 101);

            if (randomNum < badEventProbability)
            {
                potentionalEvents.Add(badEventList[Random.Range(0, badEventList.Count)]);

            }
        }

        Debug.Log("Goodevent probability " + goodEventProbability);
        Debug.Log("BadEvent probability " + badEventProbability);

        if (potentionalEvents.Count != 0)
        {
            return potentionalEvents[Random.Range(0, potentionalEvents.Count)];
        }
        else
        {
            return null;
        }
    }

    // method to create list of all random events
    public void InitaliseRandomEventLists()
    {
        badEventList.Add(new AcidRain(this));
        badEventList.Add(new Drought(this));
        badEventList.Add(new Flood(this));
        badEventList.Add(new Hurricane(this));
        badEventList.Add(new RisingSeaLevel(this));
        badEventList.Add(new Wildfire(this));
        badEventList.Add(new HeatWave(this));

        goodEventList.Add(new ForestSpawn(this));
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
        GameController.Instance.SetDelta(GenerateMoney, GenerateGreen, GenerateHappiness);


    }
    
    public void StillBuildable(Tile tile)
    {
        Debug.Log("still buildable called");
        if (tile.Building != null)
        {
            if (!tile.IsBuildable(tile.Building))
            {
                GenerateGreen = GenerateGreen - tile.Building.GenerateGreen;
                GenerateMoney = GenerateMoney - tile.Building.GenerateMoney;
                GenerateHappiness = GenerateHappiness - tile.Building.GenerateHappiness;
            }
            else
            {
                GenerateGreen = GenerateGreen + tile.Building.GenerateGreen;
                GenerateMoney = GenerateMoney + tile.Building.GenerateMoney;
                GenerateHappiness = GenerateHappiness + tile.Building.GenerateHappiness;
            }
        }
    }
}
