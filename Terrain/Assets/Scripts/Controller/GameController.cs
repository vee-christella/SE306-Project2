using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private GameGrid gameGrid;
    Game game;
    EventController eventController;
    int[,] map;
    public Game Game { get => game; protected set => game = value; }
    public EventController EventController { get => eventController; set => eventController = value; }
    public int[,] Map { get => map; set => map = value; }
    public static GameController Instance { get; protected set; }
    public GameObject tutorialOverlay;
    public GameObject[] tileGameObjs = new GameObject[4];
    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI greenCount;
    public TextMeshProUGUI happinessCount;
    public TextMeshProUGUI currentTurn;
    public TextMeshProUGUI maxTurn;
    public TextMeshProUGUI coinDeltaText;
    public TextMeshProUGUI greenDeltaText;
    public TextMeshProUGUI happinessDeltaText;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI placeholder;
    public GameObject happinessImage;

    // Set the introductions by the advisor for each level
    public GameObject level1Intro;
    public GameObject level2Intro;
    public GameObject level3Intro;

    public Sprite happyImage;
    public Sprite sadImage;


    public string[] greenMetricCheatCode = {"i","l","o","v","e","e","a","r","t","h"};
    public string[] loseCheatCode = {"p","l","a","s","t","i","c","b","a","g","s"};
    public string[] happinessCheatCode = {"h","a","p","p","y"};
    public string[] moneyCheatCode = {"r","i","c","h","b","o","y"};
    public int greenCheatIndex = 0;
    public int loseCheatIndex = 0;
    public int happinessCheatIndex = 0;
    public int moneyCheatIndex = 0;

    void Start()
    {
        Debug.Log("Game Controller Started");
        gameGrid = FindObjectOfType<GameGrid>();
        Instance = this;

        // Change the map generation depending on the game mode/difficulty
        switch (PlayerPrefs.GetInt("Level"))
        {
            case 0:
                Game = new Game(5, 5);
                Game.InitialiseMetrics(200, 0, 50, 1000);
                tutorialOverlay.SetActive(true);
                Map = TutorialLevel.Arr;
                placeholder.text = "Tutorial";
                break;
            case 1:
                Game = new Game(10, 10);
                Game.InitialiseMetrics(200, 0, 50, 1000);
                Map = PrototypeLevel.Arr;
                placeholder.text = "Level 1";
                level1Intro.SetActive(true);
                break;
            case 2:
                Game = new Game(15, 15);
                placeholder.text = "Level 2";
                level2Intro.SetActive(true);
                break;
            case 3:
                Game = new Game(20, 20);
                placeholder.text = "Level 3";
                level3Intro.SetActive(true);
                break;
            default:
                Game = new Game(5, 5);
                Game.InitialiseMetrics(200, 0, 50, 1000);
                tutorialOverlay.SetActive(true);
                Map = TutorialLevel.Arr;
                placeholder.text = "Tutorial";
                break;
        }

        Game.IsEnd = false;
        Game.HasStarted = false;

        eventController = (EventController)gameObject.GetComponentInChildren(typeof(EventController), true);
        eventController.Game = Game;
        eventController.GameController = this;

        // Populate the map with game tiles
        for (int x = 0; x < Game.Rows; x++)
        {
            for (int z = 0; z < Game.Columns; z++)
            {
                Tile tile = Game.getTileAt(x, z);

                GameObject tileGO = Instantiate(tileGameObjs[Map[x, z]]) as GameObject;
                tile.registerMethodCallbackTypeChanged((tileData) => { OnTileTypeChanged(tileData, tileGO); });

                switch (Map[x, z])
                {
                    case 0:
                        tile.setType(Tile.TileType.Desert);
                        break;
                    case 1:
                        tile.setType(Tile.TileType.Mountain);
                        break;
                    case 2:
                        tile.setType(Tile.TileType.Plain);
                        break;
                    case 3:
                        tile.setType(Tile.TileType.Water);
                        break;
                }

                // Create empty building game objects
                GameObject buildingGO = new GameObject();
                buildingGO.name = "Building(" + tile.X + ", " + tile.Y + ", " + tile.Z + ")";
                buildingGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);

                // Register the callback method for the tile so that it can be changed dynamically
                tile.registerMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingModel(tileBuildingData, buildingGO); });
            }
        }

        // Don't randomly generate world on the tutorial
        if (PlayerPrefs.GetInt("Level") != 0)
        {
            WorldGenerator.generateWorld(Game);
        }

        StartingMetrics();
        Game.HasStarted = true;
    }

    /*
    Handles the player's attempt at entering the green points cheat code.
    */
    public void greenCheat()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(greenMetricCheatCode[greenCheatIndex]))
            {
                greenCheatIndex++;
            }
            else
            {
                greenCheatIndex = 0;
            }
        }
        if (greenCheatIndex == greenMetricCheatCode.Length)
        {
            game.greenCheat();
            SetDelta(game.MoneyDelta, game.GreenDelta, game.GenerateHappiness);
            greenCheatIndex = 0;
        }
    }

    /*
    Handles the player's attempt at entering the happiness cheat code.
    */
    public void happinessCheat()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(happinessCheatCode[happinessCheatIndex]))
            {
                happinessCheatIndex++;
            }
            else
            {
                happinessCheatIndex = 0;
            }
        }
        if (happinessCheatIndex == happinessCheatCode.Length)
        {
            game.happinessCheat();
            SetDelta(game.MoneyDelta, game.GreenDelta, game.GenerateHappiness);
            happinessCheatIndex = 0;
        }
    }

    /*
    Handles the player's attempt at entering the lose game cheat code.
    */
    public void loseCheat()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(loseCheatCode[loseCheatIndex]))
            {
                loseCheatIndex++;
            }
            else
            {
                loseCheatIndex = 0;
            }
        }
        if (loseCheatIndex == loseCheatCode.Length)
        {
            game.loseCheat();
            SetDelta(game.MoneyDelta, game.GreenDelta, game.GenerateHappiness);
            loseCheatIndex = 0;
        }
    }

    public void moneyCheat()
    {
        if (Input.anyKeyDown) {
            if (Input.GetKeyDown(moneyCheatCode[moneyCheatIndex])) {
                moneyCheatIndex++;
            }
            else {
                moneyCheatIndex = 0;    
            }
        }
        if (moneyCheatIndex == moneyCheatCode.Length) {
            game.moneyCheat();
            SetDelta(game.MoneyDelta, game.GreenDelta, game.GenerateHappiness);
            moneyCheatIndex = 0;
        }
    }
    

    /*
    Handles when the end turn button is clicked.
    */
    public void callNextTurn()
    {
        Debug.Log("Next turn button clicked");
        game.NextTurn();

        if (game.GameEvent != null)
        {
            EventController.DisplayEventPopup();
        }

        Debug.Log("Finished event logic");

        SetMetrics(game.Money, game.Green, game.Happiness);
        SetDelta(game.MoneyDelta, game.GreenDelta, game.GenerateHappiness);
        SetTurn(game.CurrentTurn);
    }

    void Update()
    {
        CheckMetrics();
        greenCheat();
        loseCheat();
        happinessCheat();
        moneyCheat();
    }

    /*
    Initialise the starting metrics on the screen
    */
    public void StartingMetrics()
    {
        SetMetrics(game.Money, game.Green, game.Happiness);
        SetDelta(game.GenerateMoney, game.GenerateGreen, game.GenerateHappiness);

        game.InitialiseTurns(0, 100);
        maxTurn.text = game.MaxTurns.ToString();
        SetTurn(0);
    }

    /*
    Sets the metric text values on the metrics bar.
    */
    public void SetMetrics(float coin, float green, float happiness)
    {
        coinCount.text = System.Math.Round(coin, 2).ToString();
        greenCount.text = System.Math.Round(green, 2).ToString();
        happinessCount.text = System.Math.Round(happiness, 2).ToString();
    }

    /*
    Sets the metric delta text values on the metrics bar.
    */
    public void SetDelta(float coinDelta, float greenDelta, float happinessDelta)
    {
        if (coinDelta < 0)
        {
            coinDeltaText.text = System.Math.Round(coinDelta, 2).ToString();
        }
        else
        {
            coinDeltaText.text = "+ " + System.Math.Round(coinDelta, 2).ToString(); ;
        }

        if (greenDelta < 0)
        {
            greenDeltaText.text = System.Math.Round(greenDelta, 2).ToString(); ;

        }
        else
        {
            greenDeltaText.text = "+ " + System.Math.Round(greenDelta, 2).ToString(); ;
        }

        if (happinessDelta < 0)
        {
            happinessDeltaText.text = System.Math.Round(happinessDelta, 2).ToString(); ;
        }
        else
        {
            happinessDeltaText.text = "+ " + System.Math.Round(happinessDelta, 2).ToString(); ;
        }
    }

    /*
    Sets the current turn text and colour.
    */
    public void SetTurn(float turn)
    {
        currentTurn.text = turn.ToString();

        if (float.Parse(currentTurn.text) == 17)
        {
            currentTurn.color = new Color32(255, 150, 0, 255);
        }

        if (float.Parse(currentTurn.text) == 33)
        {
            currentTurn.color = new Color32(255, 0, 0, 255);
        }
    }

    /*
    Changes the metrics' text colours depending on whether they're positively or 
    negatively affecting the player's gameplay.
    */
    public void CheckMetrics()
    {
        if (float.Parse(coinCount.text) <= 100)
        {
            coinCount.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            coinCount.color = new Color32(0, 0, 0, 255);
        }
        if (float.Parse(greenCount.text) < 0)
        {
            greenCount.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            greenCount.color = new Color32(0, 0, 0, 255);
        }
        if (float.Parse(happinessCount.text) <= 25)
        {
            happinessCount.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            happinessCount.color = new Color32(0, 0, 0, 255);
        }

        if (coinDeltaText.text[0] == '-')
        {
            coinDeltaText.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            coinDeltaText.color = new Color32(0, 0, 0, 255);
        }

        if (greenDeltaText.text[0] == '-')
        {
            greenDeltaText.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            greenDeltaText.color = new Color32(0, 0, 0, 255);
        }

        if (happinessDeltaText.text[0] == '-')
        {
            happinessDeltaText.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            happinessDeltaText.color = new Color32(0, 0, 0, 255);
        }
    }

    /*
    Change the specified tile GameObject to a new tile GameObject with the
    specified tile type.
    */
    public void OnTileTypeChanged(Tile tile, GameObject tileGO)
    {
        // Unregeister the old tile's callback method
        tile.unregisterMethodCallbackTypeChanged((tileData) => { OnTileTypeChanged(tileData, tileGO); });

        int typeInt = 0;
        if (tile.Type == Tile.TileType.Desert)
        {
            typeInt = 0;
        }
        else if (tile.Type == Tile.TileType.Mountain)
        {
            typeInt = 1;
        }
        else if (tile.Type == Tile.TileType.Plain)
        {
            typeInt = 2;
        }
        else if (tile.Type == Tile.TileType.Water)
        {
            typeInt = 3;
        }

        // Create the new tile GameObject and set its attributes
        GameObject tileGONew = Instantiate(tileGameObjs[typeInt]) as GameObject;
        tileGONew.name = "Tile(" + tile.X + ", " + tile.Y + ", " + tile.Z + ")";
        tileGONew.transform.position = new Vector3(tile.X, tile.Y, tile.Z);

        // Set the location of the new tile GameObject
        Vector3 tileLocation = new Vector3(tile.X, tile.Y, tile.Z);
        var finalPosition = gameGrid.GetNearestPointOnGrid(tileLocation);
        tileGONew.transform.position = finalPosition;

        // Register the new tile's callback method
        tile.registerMethodCallbackTypeChanged((tileData) => { OnTileTypeChanged(tileData, tileGONew); });

        // Remove the old tile GameObject from the game
        Destroy(tileGO);
    }

    /*
    Changes the happiness image on the metrics bar.
    */
    public void ChangeImageSprite(float modifier)
    {
        if (modifier < 1)
        {
            happinessImage.GetComponent<Image>().sprite = sadImage;
        }
        else
        {
            happinessImage.GetComponent<Image>().sprite = happyImage;
        }
    }

}
