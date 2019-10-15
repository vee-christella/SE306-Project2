﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; protected set; }
    Game game;
    EventController eventController;
    private GameGrid gameGrid;
    public Game Game { get => game; protected set => game = value; }
    public EventController EventController { get => eventController; set => eventController = value; }

    // public Sprite[] sprites = new Sprite[7];

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
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Controller Started");
        gameGrid = FindObjectOfType<GameGrid>();
        Instance = this;

        Game = new Game(20, 20);
        Game.IsEnd = false;
        Game.HasStarted = false;

        eventController = (EventController)gameObject.GetComponentInChildren(typeof(EventController), true);

        // Populate the map with game tiles
        for (int x = 0; x < Game.Rows; x++)
        {
            for (int z = 0; z < Game.Columns; z++)
            {
                Tile tile = Game.getTileAt(x, z);

                int rand = Random.Range(0, 4);
                GameObject tileGO = Instantiate(tileGameObjs[rand]) as GameObject;

                // THIS SHOULD PROBABLY BE DONE IN GAME.CS WHEN TILES ARE FIRST MADE
                switch (rand)
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

                tileGO.name = "Tile(" + tile.X + ", " + tile.Y + ", " + tile.Z + ")";
                tileGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);

                Vector3 tileLocation = new Vector3(x, tile.Y, z);
                var finalPosition = gameGrid.GetNearestPointOnGrid(tileLocation);
                tileGO.transform.position = finalPosition;

                // tileSR.sprite = sprites[PrototypeLevel.Arr[i, j]];
                //Debug.Log("i = " + i + ", j = " + j);
                //Debug.Log(i == 5 && j == 4);

                tile.registerMethodCallbackTypeChanged((tileData) => { OnTileTypeChanged(tileData, tileGO); });

                // Create empty building game objects
                GameObject buildingGO = new GameObject();
                buildingGO.name = "Building(" + tile.X + ", " + tile.Y + ", " + tile.Z + ")";
                buildingGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);

                SpriteRenderer buildingSR = buildingGO.AddComponent<SpriteRenderer>();
                buildingSR.sortingLayerName = "Building";

                tile.registerMethodCallbackBuildingCreated((tileBuildingData) => { BuildingController.Instance.ChangeBuildingModel(tileBuildingData, buildingGO); });

                // Place the TownHall
                if (x == 5 && z == 4)
                {
                    BuildingController.Instance.addBuildingToTile("Town Hall", tile);
                }
            }
        }

        StartingMetrics();
        Game.HasStarted = true;
    }

    public void callNextTurn()
    {
        game.nextTurn();

        if (game.GameEvent != null)
        {
            EventController.DisplayPopup(game.GameEvent);
        }

        SetMetrics(game.Money, game.Green, game.Happiness);
        SetDelta(game.MoneyDelta, game.GreenDelta, game.GenerateHappiness);
        SetTurn(game.CurrentTurn);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMetrics();
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
        
        if (float.Parse(currentTurn.text)  == 17){
            currentTurn.color = new Color32(255,150,0,255);
        }

        if (float.Parse(currentTurn.text) == 33){
            currentTurn.color = new Color32(255,0,0,255);
        }
    }

    public void CheckMetrics()
    {
        if (float.Parse(coinCount.text) <= 100){
            coinCount.color = new Color32(255,0,0,255);
        } else {
            coinCount.color = new Color32(0,0,0,255);
        }
        if (float.Parse(greenCount.text) < 0){
            greenCount.color = new Color32(255,0,0,255);
        } else {
            greenCount.color = new Color32(0,0,0,255);
        }
        if (float.Parse(happinessCount.text) <= 25){
            happinessCount.color = new Color32(255,0,0,255);
        } else {
            happinessCount.color = new Color32(0,0,0,255);
        }
        
        if (coinDeltaText.text[0] == '-'){
            coinDeltaText.color = new Color32(255,0,0,255);
        } else {
            coinDeltaText.color = new Color32(0,0,0,255);
        }

        if (greenDeltaText.text[0] == '-'){
            greenDeltaText.color = new Color32(255,0,0,255);
        } else {
            greenDeltaText.color = new Color32(0,0,0,255);
        }

        if (happinessDeltaText.text[0] == '-'){
            happinessDeltaText.color = new Color32(255,0,0,255);
        } else {
            happinessDeltaText.color = new Color32(0,0,0,255);
        }
    }

    public void OnTileTypeChanged(Tile tile, GameObject tileGO)
    {
        //Debug.Log("on tile type changed");
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
        // tileGO.GetComponent<SpriteRenderer>().sprite = sprites[random];

    }

    public void ShowError(string textToShow)
    {

        //errorText = (TextMeshProUGUI)errorMessage.GetComponentInChildren(typeof(TextMeshProUGUI), true);
        errorText.text = textToShow;
        errorMessage.SetActive(true);
    }

}
