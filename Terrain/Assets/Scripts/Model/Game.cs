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
    bool isUnhappy = false;
    float moneyDelta;
    float greenDelta;
    bool hasStarted = false;

    public float modifier = 1;
    float prevMoney;
    float prevHappiness;
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

    public float MoneyDelta { get => moneyDelta; set => moneyDelta = value; }
    public float GreenDelta { get => greenDelta; set => greenDelta = value; }
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


        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < columns; z++)
            {
            
                Debug.Log("game created");
                tiles[x, z] = new Tile(this, x, z);
                tiles[x, z].registerMethodCallbackTypeChanged(StillBuildable);
            }
        }
    }


    public Tile getTileAt(int x, int z)
    {
        if (x >= rows || x < 0 || z >= columns || z < 0)
        {
            return null;
        }

        return tiles[x, z];
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

    public void greenCheat()
    {
        GreenDelta = 1000;
        GenerateGreen = 1000;
    }

    public void loseCheat()
    {
        MoneyDelta = -1000;
        GenerateMoney = -1000;
    }

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
                Debug.Log("==== Game not null = " + building != null);
                buildings[tile.X, tile.Z] = building;
                UpdateMetrics(building);
                if(buildingType == "Oil Refinery"){
                    AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.BuildOlilRig);
                }
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
        
        goodEventProbability = Mathf.Floor((green / 2000 * 100)/2f);

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
        goodEventList.Add(new Circus(this));
    }

    // Change the metrics with regards to the effects of the building
    // that has just been placed.
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


    private void getModifier(float happinessDelta)
    {
        if (Happiness >= 50 && Happiness + happinessDelta < 50)
        {
            Debug.Log("50 down");

            modifier -= (float)0.1;
        }

        if (Happiness < 50 && Happiness + happinessDelta >= 50)
        {
            Debug.Log("50 up");

            modifier += (float)0.1;

        }

        if (Happiness >= 30 && Happiness + happinessDelta < 30)
        {
            Debug.Log("30 down");

            modifier -= (float)0.1;
        }

        if (Happiness < 30 && Happiness + happinessDelta >= 30)
        {
            Debug.Log("30 up");

            modifier += (float)0.1;
        }

        if (Happiness < 70 && Happiness + happinessDelta >= 70)
        {
            Debug.Log("70 up");

            modifier += (float)0.1;
        }

        if (Happiness >= 70 && Happiness + happinessDelta < 70)
        {
            Debug.Log("70 down");

            modifier -= (float)0.1;
        }

        if (Happiness < 90 && Happiness + happinessDelta >= 90)
        {
            Debug.Log("90 up");

            modifier += (float)0.1;
        }

        if (Happiness >= 90 && Happiness + happinessDelta < 90)
        {
            Debug.Log("90 down");
            modifier -= (float)0.1;
        }
    }

    private void calculateDelta()
    {
        if (GenerateMoney > 0)
        {
            moneyDelta = GenerateMoney * modifier;
        }
        else
        {
            moneyDelta = GenerateMoney * (1 / modifier);
        }

        if (GenerateGreen > 0)
        {
            greenDelta = GenerateGreen * modifier;
        }
        else
        {
            greenDelta = GenerateGreen * (1 / modifier);
            Debug.Log("greendelta middle: " + greenDelta);
        }
        moneyDelta = (float)System.Math.Round(moneyDelta, 2);
        greenDelta = (float)System.Math.Round(greenDelta, 2);
    }
}
