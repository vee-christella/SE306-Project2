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
    public GameObject errorMessage;
    public string[] greenMetricCheatCode = { "i", "l", "o", "v", "e", "e", "a", "r", "t", "h" };
    public string[] loseCheatCode = { "p", "l", "a", "s", "t", "i", "c", "b", "a", "g", "s" };
    public string[] happinessCheatCode = { "h", "a", "p", "p", "y" };
    public int greenCheatIndex = 0;
    public int loseCheatIndex = 0;
    public int happinessCheatIndex = 0;

    // Start is called before the first frame update
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

                tile.registerMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingModel(tileBuildingData, buildingGO); });

                // Place the TownHall
                if (x == 4 && z == 5)
                {
                    BuildingController.Instance.addBuildingToTile("Town Hall", tile);
                }
            }
        }

        // Don't generate world on tutorial
        if (PlayerPrefs.GetInt("Level") != 0)
        {
            WorldGenerator.generateWorld(Game);
        }

        StartingMetrics();
        Game.HasStarted = true;
    }

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

    // Update is called once per frame
    void Update()
    {
        CheckMetrics();
        greenCheat();
        loseCheat();
        happinessCheat();
    }

    // Initialise the starting metrics on the screen
    public void StartingMetrics()
    {
        game.InitialiseMetrics(200, 0, 50, 1000);
        SetMetrics(game.Money, game.Green, game.Happiness);
        SetDelta(0, 0, 0);

        game.InitialiseTurns(0, 50);
        maxTurn.text = game.MaxTurns.ToString();
        SetTurn(0);
    }

    public void SetMetrics(float coin, float green, float happiness)
    {
        coinCount.text = coin.ToString();
        greenCount.text = green.ToString();
        happinessCount.text = happiness.ToString();
    }

    public void SetDelta(float coinDelta, float greenDelta, float happinessDelta)
    {

        if (coinDelta < 0)
        {
            coinDeltaText.text = coinDelta.ToString();
        }
        else
        {
            coinDeltaText.text = "+ " + coinDelta.ToString();
        }

        if (greenDelta < 0)
        {
            greenDeltaText.text = greenDelta.ToString();

        }
        else
        {
            greenDeltaText.text = "+ " + greenDelta.ToString();
        }

        if (happinessDelta < 0)
        {
            happinessDeltaText.text = happinessDelta.ToString();
        }
        else
        {
            happinessDeltaText.text = "+ " + happinessDelta.ToString();
        }
    }

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

    public void OnTileTypeChanged(Tile tile, GameObject tileGO)
    {
        //Debug.Log("on tile type changed");
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
        GameObject tileGONew = Instantiate(tileGameObjs[typeInt]) as GameObject;
        tileGONew.name = "Tile(" + tile.X + ", " + tile.Y + ", " + tile.Z + ")";
        tileGONew.transform.position = new Vector3(tile.X, tile.Y, tile.Z);
        Vector3 tileLocation = new Vector3(tile.X, tile.Y, tile.Z);
        var finalPosition = gameGrid.GetNearestPointOnGrid(tileLocation);
        tileGONew.transform.position = finalPosition;
        tile.registerMethodCallbackTypeChanged((tileData) => { OnTileTypeChanged(tileData, tileGONew); });
        Destroy(tileGO);

    }

    public void ShowError(string textToShow)
    {

        //errorText = (TextMeshProUGUI)errorMessage.GetComponentInChildren(typeof(TextMeshProUGUI), true);
        errorText.text = textToShow;
        errorMessage.SetActive(true);
    }

}
