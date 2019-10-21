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
    float money;
    float greenPoints;
    float happiness;
    float prevMoney;
    float prevHappiness;
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
    public float modifier = 1.0f;
    GameObject errorMessage;
    bool levelOneComplete = false;
    bool levelTwoComplete = false;
    bool levelThreeComplete = false;
    public int Rows { get => rows; }
    public int Columns { get => columns; }
    public Tile[,] Tiles { get => tiles; set => tiles = value; }
    public float Money { get => money; set => money = value; }
    public float Green { get => greenPoints; set => greenPoints = value; }
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
        /*
        switch (PlayerPrefs.GetInt("Level"))
        {
            case 0:
                Game = new Game(5, 5);
                tutorialOverlay.SetActive(true);
                Map = TutorialLevel.Arr;
                break;
            case 1:
                Game = new Game(10, 10);
                Map = PrototypeLevel.Arr;
                break;
            case 2:
                Game = new Game(15, 15);
                break;
            case 3:
                Game = new Game(20, 20);
                break;
            default:
                Game = new Game(5, 5);
                tutorialOverlay.SetActive(true);
                Map = TutorialLevel.Arr;
                break;

        }
        */


        this.isEnd = false;
        this.currentTurn = 0;
        this.rows = rows;
        this.columns = columns;

        Tiles = new Tile[rows, columns];

        InitaliseRandomEventLists();

        // Create the game tiles
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < columns; z++)
            {
                tiles[x, z] = new Tile(this, x, z);
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

    public void moneyCheat()
    {
        MoneyDelta = 1000;
        GenerateMoney = 1000;
    }

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

    /* Proceeds with the next turn after the user clicks the end turn button. The acumulated money, green
    points and happiness are shown, and the corresponding values on the UI are updated.
    */
    public void NextTurn()
    {
        if (currentTurn == 0)
        {
            modifier = 1.0f;
        }
        this.currentTurn++;

        getModifier(GenerateHappiness);
        Debug.Log("hapGen: " + GenerateHappiness);
        Happiness = Happiness + GenerateHappiness;
        if(Happiness > 100)
        {
            AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.Happiness);
            Happiness = 100;
        } else if (Happiness < 0)
        {
            Happiness = 0;
        }
        GameController.Instance.ChangeImageSprite(modifier);

        // If Happiness is >100 or <0, set it to 100 or 0 respectively 
        Happiness = (Happiness > 100) ? 100 : Happiness;
        Happiness = (Happiness < 0) ? 0 : Happiness;

        // Update the metrics
        Money = Money + moneyDelta;
        Green = Green + greenDelta;

        // Check if the user has won the game by reaching the number of green points required
        if (this.greenPoints >= maxGreen)
        {
            //Check Achievements
            AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.Win);
            AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.Win5);

            if(currentTurn <= 80) 
            {
                AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.Win80Turns);
            }
            if(currentTurn <= 90)
            {
                AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.Win90Turns);
            }
            if (PlayerPrefs.GetInt("Level", 0) == 1 && !levelOneComplete)
            {
                Debug.Log("Complete level 1");
                levelOneComplete = true;
                AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.WinAllLevels);
            }
            if (PlayerPrefs.GetInt("Level", 0) == 2 && !levelTwoComplete)
            {
                Debug.Log("Complete level 2");
                levelTwoComplete = true;
                AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.WinAllLevels);
            }
            if (PlayerPrefs.GetInt("Level", 0) == 3 && !levelThreeComplete)
            {
                Debug.Log("Complete level 3");
                levelThreeComplete = true;
                AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.WinAllLevels);
            }

            this.endGame(true);
            return;
        }
        // Check if the user has lost the game by exceeding the max number of turns allowed, or having a no money left
        else if (currentTurn >= maxTurns || Money < 0)
        {
            AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.Lose5);
            this.endGame(false);
            return;
        }

        // Get a random event to occur (if any)
        GameEvent = EventForNextTurn();

        if (GameEvent != null && GameEvent.GetType().Name.ToString() == "RisingSeaLevel" || GameEvent != null && GameEvent.GetType().Name.ToString() == "EarthQuake")
        {
            // The bad events "RisingSeaLevel" "EarthQuake" should only occur once per game
            badEventList.Remove(GameEvent);
        }

        // If has occured update the metrics
        if (GameEvent != null)
        {
            Money = Money + GameEvent.MoneyDelta;
            Green = Green + GameEvent.GreenPointDelta;
            Happiness = Happiness + GameEvent.HappinessDelta;
        }
        calculateDeltas();
    }

    /*
    Sets the booleans for the end of the game. These are used by the EndscreenController to check
    when to show the end game popup.
    */
    public void endGame(bool isVictory)
    {
        this.isEnd = true;
        this.IsVictory = isVictory;
        GameController.Instance.PlayEndSound(isVictory);
    }

    /*
    Adds the specified building to the specified tile.
    */
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
                // The building was successfully built
                UpdateMetrics(building);
                if(PlayerPrefs.GetInt("ActivateAchievement", 0) == 0 && buildingName == "Oil Refinery"){
                    AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.BuildOilRig);
                }
                if(PlayerPrefs.GetInt("ActivateAchievement", 0) == 0 && buildingName == "Nuclear Plant"){
                    AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.BuildNuclear);
                }
                if(PlayerPrefs.GetInt("ActivateAchievement", 0) == 0 && buildingName == "Hydro Plant"){
                    AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.BuildHydro);
                }
                return building;
            }
        }
        else
        {
            GameController.Instance.errorPopup.SetActive(true);
        }

        // The building was not built
        return null;
    }

    /*
    Sell the building on the specified tile.
    */
    public void SellBuilding(Tile tile)
    {
        Building building = tile.Building;
        float CostToSell;

        switch (PlayerPrefs.GetInt("Level"))
        {
            case 1:
                CostToSell = building.InitialBuildMoney * 0.3f * -1;
                break;
            case 2:
                CostToSell = building.InitialBuildMoney * 0.2f * -1;
                break;
            case 3:
                CostToSell = building.InitialBuildMoney * 0.05f * -1;
                break;
            default:
                CostToSell = building.InitialBuildMoney * 0.1f * -1;
                break;
        }
        if(Money + CostToSell >= 0)
        {
            // Check that there's a building to remove/that it's been removed successfully
            if (tile.removeBuilding())
            {
                getModifier(building.InitialBuildHappiness * -1);

                // Update the metrics and metric deltas
                Money += CostToSell;
                Happiness -= building.InitialBuildHappiness;
                if (tile.IsBuildable(building))
                {
                    Debug.Log("Selling Building Modify Gen");
                    GenerateHappiness -= building.GenerateHappiness;
                    GenerateMoney -= building.GenerateMoney;
                    GenerateGreen -= building.GenerateGreen;
                }

                calculateDeltas();
                Debug.Log("Modifier: " + modifier);

                // Set the metrics and metric deltas on the UI
                GameController.Instance.SetMetrics(Money, Green, Happiness);
                GameController.Instance.SetDelta(moneyDelta, greenDelta, GenerateHappiness);
            }
        }
    }

    /*
    Gets the random event (either good or bad) that will occur in the next turn. Events will have a 
    certain probability of occuring, and the probability will be affected by how far the player is into 
    the game and how many green points the player currently has. There is also a chance no event will occur.
    - Higher turn number        -> Increased probability
    - More green points         -> Increased good event probability and decreased bad event probability
    - Higher game difficulty    -> Increased probability
    */
    public Event EventForNextTurn()
    {
        List<Event> potentionalEvents = new List<Event>();
        float badEventProbability = 0;
        float goodEventProbability;
        float difficultyOffset = 0.1f;

        switch (PlayerPrefs.GetInt("Level"))
        {
            case 1:
                difficultyOffset = 0.1f;
                return null;
            case 2:
                difficultyOffset = 0.2f;
                break;
            case 3:
                difficultyOffset = 0.3f;
                break;
            default:
                difficultyOffset = 0.1f;
                break;
        }

        // Calculate the green points effect on good/bad event probability
        goodEventProbability = Mathf.Floor((1.3f-difficultyOffset)*(greenPoints / 2000 * 100) / 2f);
        badEventProbability = (greenPoints < 0) ? (1 - 700 / (1000 - greenPoints)) : (300 / (1000 + greenPoints));

        // Calculate the game difficulty and turn number effect on bad event probability
        badEventProbability = Mathf.FloorToInt(((difficultyOffset + (0.7f * badEventProbability)) * (currentTurn / maxTurns)) * 100);

        // If bad event probability is >80% (maximum), then a good event cannot occur
        if (badEventProbability > 100)
        {
            goodEventProbability = 0;
            badEventProbability = 80;
        }

        Debug.Log("GoodEvent probability " + goodEventProbability);
        Debug.Log("BadEvent probability " + badEventProbability);

        // Good events occuring take priority over bad events
        if (Random.Range(1, 101) < goodEventProbability)
        {
            potentionalEvents.Add(goodEventList[Random.Range(0, goodEventList.Count)]);
        }
        else if (Random.Range(1, 101) < badEventProbability)
        {
            potentionalEvents.Add(badEventList[Random.Range(0, badEventList.Count)]);
        }

        // If an event can occur, choose a random one to happen, otherwise no event will occur
        return (potentionalEvents.Count != 0) ? potentionalEvents[Random.Range(0, potentionalEvents.Count)] : null;
    }

    /*
    Populates the bad event list with all the bad random events, and the good event list
    with the good random events.
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
        badEventList.Add(new EarthQuake(this));

        goodEventList.Add(new ForestSpawn(this));
        goodEventList.Add(new Circus(this));
        goodEventList.Add(new Rejuvenation(this));

    }

    /*
    Changes the metrics with regards to the effects of a building that has just been placed.
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
        GameController.Instance.ChangeImageSprite(modifier);


        GenerateMoney += building.GenerateMoney;
        GenerateGreen += building.GenerateGreen;
        GenerateHappiness += building.GenerateHappiness;

        Debug.Log("Happiness: " + Happiness);

        Debug.Log("Building Happ: " + building.InitialBuildHappiness);

        calculateDeltas();

        Debug.Log("Modifier: " + modifier);

        GameController.Instance.SetMetrics(Money, Green, Happiness);
        GameController.Instance.SetDelta(MoneyDelta, GreenDelta, GenerateHappiness);
    }

    /*
    Checks to see if a building is still supposed to be buildable on top of the tile.
    The metrics will be update after the check based on whether the building should
    be on the tile or not.This is required because of the random events that change the
    tiles on the map. For example, a drought can change a water tile to a desert tile,
    and a hydro dam built on the tile will still remain on the tile. Since a hydro dam
    wouldn't function without water (as is not buildable on a desert tile), it makes sense 
    to negate that building's metric effects, essentially rendering the building useless.
    */
    public void StillBuildable(Tile tile)
    {
        if (tile.Building != null)
        {
            Debug.Log("TIle type was: " + tile.wasBuildable(tile.Building) + " type is: " + tile.IsBuildable(tile.Building));
            if (!tile.wasBuildable(tile.Building) && tile.IsBuildable(tile.Building))
            {
                GenerateGreen = GenerateGreen + tile.Building.GenerateGreen;
                GenerateMoney = GenerateMoney + tile.Building.GenerateMoney;
                GenerateHappiness = GenerateHappiness + tile.Building.GenerateHappiness;
            }
            else if (tile.wasBuildable(tile.Building) && !tile.IsBuildable(tile.Building))
            {
                GenerateGreen = GenerateGreen - tile.Building.GenerateGreen;
                GenerateMoney = GenerateMoney - tile.Building.GenerateMoney;
                GenerateHappiness = GenerateHappiness - tile.Building.GenerateHappiness;
                Debug.Log("Green: " + GenerateGreen + " Mon: " + GenerateMoney + "Hap: " + GenerateHappiness);
            }
        }
        calculateDeltas();
    }

    /*
    Changes the modifier based on the current happiness and the happiness delta.
    */
    private void getModifier(float happinessDelta)
    {

        Debug.Log("Modifier Before Hap: " + modifier);
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

        if (Happiness >= 70 && Happiness + happinessDelta < 70)
        {
            Debug.Log("70 down");
            modifier -= 0.1f;
        }

        if (Happiness < 70 && Happiness + happinessDelta >= 70)
        {
            Debug.Log("70 up");
            modifier += 0.1f;
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
        Debug.Log("Modifier After Hap: " + modifier);
    }

    /*
    Calculate the delta values for money and green points (i.e. how money and 
    green points will change per turn after happiness modifiers have been added).
    */
    private void calculateDeltas()
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
            Debug.Log("greendelta middle: " + greenDelta+ " modifier: " + modifier);
        }
        Debug.Log("MonGen: " + GenerateMoney + " GreenGen: " + GenerateGreen);

        // Round to 2 decimal places
        moneyDelta = (float)System.Math.Round(moneyDelta, 2);
        greenDelta = (float)System.Math.Round(greenDelta, 2);

    }

}