using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; protected set; }
    Game game;
    EventController eventController;
    int[,] map;



    public Game Game { get => game; protected set => game = value; }
    public EventController EventController { get => eventController; set => eventController = value; }
    public int[,] Map { get => map; set => map = value; }




    public Sprite[] sprites = new Sprite[7];

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


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        // Change the map generation depending on the game mode/difficulty
        switch (PlayerPrefs.GetInt("Level"))
        {
            case 0:
                Game = new Game(5, 5);
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
        }

        eventController = (EventController)gameObject.GetComponentInChildren(typeof(EventController), true);
        for (int i = 0; i < Game.Rows; i++)
        {
            for (int j = 0; j < Game.Columns; j++)
            {
                Tile tile = Game.getTileAt(i, j);

                GameObject tileGO = new GameObject();
                tileGO.name = "Tile(" + i + ", " + j + ")";
                tileGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);

                SpriteRenderer tileSR = tileGO.AddComponent<SpriteRenderer>();
                tileSR.sortingLayerName = "Tile";

                int random = Random.Range(0, 7);

                // Use a different map arrangement depending on the level


                switch (Map[i, j])
                {
                    case 0:
                    case 1:
                        tile.setType(Tile.TileType.Desert);
                        break;
                    case 2:
                    case 3:
                        tile.setType(Tile.TileType.Mountain);
                        break;
                    case 4:
                    case 5:
                        tile.setType(Tile.TileType.Plain);
                        break;
                    case 6:
                        tile.setType(Tile.TileType.Water);
                        break;
                }

                tileSR.sprite = sprites[Map[i, j]];
                tile.registerMethodCallbackTypeChanged((tileData) => { OnTileTypeChanged(tileData, tileGO); });


                Debug.Log("i = " + i + ", j = " + j);
                Debug.Log(i == 5 && j == 4);

                GameObject buildingGO = new GameObject();
                buildingGO.name = "Building(" + tile.X + ", " + tile.Y + ")";
                buildingGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);
                SpriteRenderer buildingSR = buildingGO.AddComponent<SpriteRenderer>();
                buildingSR.sortingLayerName = "Building";
                tile.registerMethodCallbackBuildingCreated((titleBuildingData) => { OnBuildingChange(titleBuildingData, buildingGO); });

                // Place the TownHall
                if (i == 5 && j == 4)
                {
                    BuildingController.Instance.addBuildingToTile("Town Hall", tile);
                }

                
            }
        }

        StartingMetrics();
        Camera.main.transform.position = new Vector3(game.Columns / 2, game.Rows / 2, -10);

    }

    public void callNextTurn()
    {
        game.nextTurn();

        if (game.GameEvent != null)
        {
            EventController.DisplayPopup(game.GameEvent);
        }

        SetMetrics(game.Money, game.Green, game.Happiness);
        SetDelta(game.GenerateMoney, game.GenerateGreen, game.GenerateHappiness);
        SetTurn(game.CurrentTurn);
    }

    // Update is called once per frame
    void Update()
    {

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
        if(coinDelta < 0)
        {
            coinDeltaText.text = coinDelta.ToString();
        }
        else
        {
            coinDeltaText.text = "+ " + coinDelta.ToString();
        }

        if(greenDelta < 0)
        {
            greenDeltaText.text = greenDelta.ToString();

        } else
        {
            greenDeltaText.text = "+ " + greenDelta.ToString();
        }

        if (happinessDelta < 0)
        {
            happinessDeltaText.text = happinessDelta.ToString();
        } else
        {
            happinessDeltaText.text = "+ " + happinessDelta.ToString();
        }
    }

    public void SetTurn(float turn)
    {
        currentTurn.text = turn.ToString();
    }

    public void OnTileTypeChanged(Tile tile, GameObject tileGO)
    {
        Debug.Log("on tile type changed");
        int random = 0;
        if (tile.Type == Tile.TileType.Desert)
        {
            random = Random.Range(0, 1);
        }
        else if (tile.Type == Tile.TileType.Mountain)
        {
            random = Random.Range(2, 3);
        }
        else if (tile.Type == Tile.TileType.Plain)
        {
            random = Random.Range(4, 5);
        }
        else if (tile.Type == Tile.TileType.Water)
        {
            random = 6;
        }
        tileGO.GetComponent<SpriteRenderer>().sprite = sprites[random];

    }

    public void ShowError(string textToShow)
    { 

        //errorText = (TextMeshProUGUI)errorMessage.GetComponentInChildren(typeof(TextMeshProUGUI), true);
        errorText.text = textToShow;
        errorMessage.SetActive(true);
    }


    public void OnBuildingChange(Tile tile, GameObject buildingGO)
    {
        BuildingController.Instance.ChangeBuildingSprite(tile, buildingGO);
    }
}
