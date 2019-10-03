﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; protected set; }
    Game game;
    public Game Game { get => game; protected set => game = value; }

    public Sprite[] sprites = new Sprite[4];

    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI greenCount;
    public TextMeshProUGUI happinessCount;
    public TextMeshProUGUI currentTurn;
    public TextMeshProUGUI maxTurn;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Game = new Game();
        for (int i = 0; i < Game.Rows; i++)
        {
            for (int j = 0; j < Game.Columns; j++)
            {
                Tile tile = Game.getTileAt(i, j);
                GameObject tileGO = new GameObject();
                tileGO.name = "Tile(" + i + ", " + j + ")";
                tileGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);
                SpriteRenderer tileSR = tileGO.AddComponent<SpriteRenderer>();
                tileSR.sortingLayerName="Tile";
                int random = Random.Range(0, 4);
                switch (random)
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
                    case 4:
                        tile.setType(Tile.TileType.Plain);
                        break;
                }
                tileSR.sprite = sprites[random];
            }
        }

        StartingMetrics();
        Camera.main.transform.position = new Vector3(game.Columns / 2, game.Rows / 2, -10);

    }

    public void callNextTurn()
    {
        game.nextTurn();
        SetMetrics(game.Money, game.Green, game.Happiness);
        SetTurn(game.CurrentTurn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartingMetrics()
    {
        coinCount.text = "200";
        greenCount.text = "0";
        happinessCount.text = "50";
        currentTurn.text = "0";
        maxTurn.text = "50";
    }

    public void SetMetrics(float coin, float green, float happiness)
    {
        //coinCount.text = (System.Int32.Parse(coinCount.text) + 10).ToString();
        //greenCount.text = (System.Int32.Parse(greenCount.text) + 10).ToString();
        //happinessCount.text = (System.Int32.Parse(happinessCount.text) + 10).ToString();

        coinCount.text = coin.ToString();
        greenCount.text = green.ToString();
        happinessCount.text = happiness.ToString();
    }

    public void SetTurn(float turn)
    {
        currentTurn.text = turn.ToString();
    }
}
