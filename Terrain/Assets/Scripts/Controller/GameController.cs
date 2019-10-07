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

    public Game Game { get => game; protected set => game = value; }
    public EventController EventController { get => eventController; set => eventController = value; }

    public Sprite[] sprites = new Sprite[7];

    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI greenCount;
    public TextMeshProUGUI happinessCount;
    public TextMeshProUGUI currentTurn;
    public TextMeshProUGUI maxTurn;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Game = new Game(10, 10);
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

                switch (PrototypeLevel.Arr[i, j])
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

                tileSR.sprite = sprites[PrototypeLevel.Arr[i, j]];
                tile.registerMethodCallbackTypeChanged((tileData) => { OnTileTypeChanged(tileData, tileGO); });


                Debug.Log("i = " + i + ", j = " + j);
                Debug.Log(i == 4 && j == 7);

                // Place the TownHall
                if (i == 8 && j == 5)
                {
                    Debug.Log("BANANA");
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
}
