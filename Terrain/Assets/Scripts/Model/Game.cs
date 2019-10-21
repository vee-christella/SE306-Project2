using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*
This class controls the logic and data related to the game.
*/
public class Game
{
    public enum GameDifficulty { Easy, Medium, Hard };
    GameDifficulty gameDifficulty;
    int rows;
    int columns;
    Tile[,] tiles;
    Building[,] buildings;
    float money;
    float green;
    float happiness;
    float prevMoney;
    float prevHappiness;
    bool isUnhappy = false;
    // generateX is the amount generated per turn before happiness modifiers
    float generateMoney;
    float generateGreen;
    float generateHappiness;
    // xDelta is the amount generated per turn after happiness modifiers
    float moneyDelta;
    float greenDelta;
    Event gameEvent;
    float currentTurn;
    float maxTurns;
    float maxGreen;
    bool isEnd;
    bool isVictory;
    List<Event> goodEventList = new List<Event>();
    List<Event> badEventList = new List<Event>();
    bool hasStarted = false;
    public float modifier = 1;
    GameObject errorMessage;

    public int Rows { get => rows; }
    public int Columns { get => columns; }
    public Tile[,] Tiles { get => tiles; set => tiles = value; }
    public float Money { get => money; set => money = value; }
    public float Green { get => green; set => green = value; }
    public float Happiness { get => happiness; set => happiness = value; }
    public float GenerateMoney { get => generateMoney; set => generateMoney = value; }
    public float GenerateGreen { get => generateGreen; set => generateGreen = value; }
    public float GenerateHappiness { get => generateHappiness; set => generateHappiness = value; }
    public float MoneyDelta { get => moneyDelta; set => moneyDelta = value; }
    public float GreenDelta { get => greenDelta; set => greenDelta = value; }
    public Event GameEvent { get => gameEvent; set => gameEvent = value; }
    public float CurrentTurn { get => currentTurn; set => currentTurn = value; }
    public float MaxTurns { get => maxTurns; set => maxTurns = value; }
    public float MaxGreen { get => maxGreen; set => maxGreen = value; }
    public bool IsEnd { get => isEnd; set => isEnd = value; }
    public bool IsVictory { get => isVictory; set => isVictory = value; }
    public bool HasStarted { get => hasStarted; set => hasStarted = value; }

    public Game(int rows, int columns)
    {
        this.isEnd = false;
        this.currentTurn = 0;
        this.rows = rows;
        this.columns = columns;

        Tiles = new Tile[rows, columns];
        buildings = new Building[rows, columns];

        InitaliseRandomEventLists();

        // Create the game tiles
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < columns; z++)
            {
                tiles[x, z] = new Tile(this, x, z);
                // Register a callback method for each tile so that it can be changed dynamically
                tiles[x, z].registerMethodCallbackTypeChanged(StillBuildable);
            }
        }

        Debug.Log("game created");
    }

    /*
    Gets the tile at the specified (x, z) coordinates on the game map
    */
    public Tile getTileAt(int x, int z)
    {
        if (x >= rows || x < 0 || z >= columns || z < 0)
        {
            return null;
        }

        return tiles[x, z];
    }

    /*
    Sets the inital metrics for money, green points, happiness and the green point goal for winning the game.
    */
    public void InitialiseMetrics(float money, float green, float happiness, float maxGreen)
    {
        Money = money;
        Green = green;
        Happiness = happiness;
        MaxGreen = maxGreen;
    }

    /*
    Sets the inital turn number and the maximum number of turns for the game
    */
    public void InitialiseTurns(float currentTurn, float maxTurn)
    {
        CurrentTurn = currentTurn;
        MaxTurns = maxTurn;
    }

    /*
    Cheat for getting more green points and a 1000 green point delta
    */
    public void greenCheat()
    {
        GreenDelta = 1000;
        GenerateGreen = 1000;
    }

    /*
    Cheat for losing the game
    */
    public void loseCheat()
    {
        MoneyDelta = -1000;
        GenerateMoney = -1000;
    }

    /*
    Cheat for getting maximum happiness
    */
    public void happinessCheat()
    {
        GenerateHappiness = 100;
    }

    /* This method proceeds with the next turn after the user clicks the 
     * end turn button. It increments the accumulated points and shows it on 
     * the metrics
     */
    public void NextTurn()
    {
        this.currentTurn++;
        Debug.Log("turn " + this.currentTurn);

        getModifier(GenerateHappiness);

        Happiness = Happiness + GenerateHappiness;

        // Increase the metrics
        calculateDelta();

        Money = Money + moneyDelta;
        Green = Green + greenDelta;
        Debug.Log(greenDelta);


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

    public Building addBuildingToTile(string buildingName, Tile tile)
    {
        string buildingClassName = Building.resolveBuildingClassName(buildingName);

        // Create the building object for the specified building class name
        Building building = (Building)System.Activator.CreateInstance(System.Type.GetType(buildingClassName));

        // Check if funds are sufficient
        if (Money + building.InitialBuildMoney >= 0)
        {
            if (tile.placeBuilding(building))
            {
                Debug.Log("==== Game not null = " + building != null);
                buildings[tile.X, tile.Z] = building;
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
            getModifier(building.InitialBuildHappiness * -1);
            Happiness -= building.InitialBuildHappiness;
            GenerateHappiness -= building.GenerateHappiness;
            GenerateMoney -= building.GenerateMoney;
            GenerateGreen -= building.GenerateGreen;

            calculateDelta();
            Debug.Log("Modifier: " + modifier);

            GameController.Instance.SetMetrics(Money, Green, Happiness);
            GameController.Instance.SetDelta(moneyDelta, greenDelta, GenerateHappiness);

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

        goodEventProbability = Mathf.Floor((green / 2000 * 100) / 2f);

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

    /*
    Creates a list of all random events
    */
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
        goodEventList.Add(new Circus(this));
    }

    /*
    Changes the metrics with regards to the effects of the building
    that has just been placed.
    */
    public void UpdateMetrics(Building building)
    {

        Money += building.InitialBuildMoney;
        Green += building.InitialBuildGreen;

        getModifier(building.InitialBuildHappiness);
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

        Debug.Log("Happiness: " + Happiness);

        Debug.Log("Building Happ: " + building.InitialBuildHappiness);

        calculateDelta();

        Debug.Log("Modifier: " + modifier);

        GameController.Instance.SetMetrics(Money, Green, Happiness);
        GameController.Instance.SetDelta(MoneyDelta, GreenDelta, GenerateHappiness);
    }

    /*
    
    */
    public void StillBuildable(Tile tile)
    {
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

    /*
    Changes the modifier based on the current happiness and the
    happiness delta.
    */
    private void getModifier(float happinessDelta)
    {
        if (Happiness >= 30 && Happiness + happinessDelta < 30)
        {
            Debug.Log("30 down");
            modifier -= 0.1f;
        }

        if (Happiness < 30 && Happiness + happinessDelta >= 30)
        {
            Debug.Log("30 up");
            modifier += 0.1f;
        }

        if (Happiness >= 50 && Happiness + happinessDelta < 50)
        {
            Debug.Log("50 down");
            modifier -= 0.1f;
        }

        if (Happiness < 50 && Happiness + happinessDelta >= 50)
        {
            Debug.Log("50 up");
            modifier += 0.1f;
        }

        if (Happiness < 70 && Happiness + happinessDelta >= 70)
        {
            Debug.Log("70 up");
            modifier += 0.1f;
        }

        if (Happiness >= 70 && Happiness + happinessDelta < 70)
        {
            Debug.Log("70 down");
            modifier -= 0.1f;
        }

        if (Happiness < 90 && Happiness + happinessDelta >= 90)
        {
            Debug.Log("90 up");
            modifier += 0.1f;
        }

        if (Happiness >= 90 && Happiness + happinessDelta < 90)
        {
            Debug.Log("90 down");
            modifier -= 0.1f;
        }
    }

    /*
    Calculate the delta values for money and green points (i.e. how money and 
    green points will change per turn after happiness modifiers have been added).
    */
    private void calculateDelta()
    {
        // Calculate money delta
        if (GenerateMoney > 0)
        {
            moneyDelta = GenerateMoney * modifier;
        }
        else
        {
            moneyDelta = GenerateMoney * (1 / modifier);
        }

        // Calculate green points delta
        if (GenerateGreen > 0)
        {
            greenDelta = GenerateGreen * modifier;
        }
        else
        {
            greenDelta = GenerateGreen * (1 / modifier);
            Debug.Log("greendelta middle: " + greenDelta);
        }

        // Round to 2 decimal places
        moneyDelta = (float)System.Math.Round(moneyDelta, 2);
        greenDelta = (float)System.Math.Round(greenDelta, 2);
    }

}