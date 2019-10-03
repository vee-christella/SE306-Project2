using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Game game;

    public Sprite[] sprites = new Sprite[4];

    public Text coinCount;
    public Text greenCount;
    public Text happinessCount;
    public Text currentTurn;
    public Text maxTurn;
    // Start is called before the first frame update
    void Start()
    {
        game = new Game();
        for (int i = 0; i < game.Rows; i++)
        {
            for (int j = 0; j < game.Columns; j++)
            {
                Tile tile = game.getTileAt(i, j);
                GameObject tileGO = new GameObject();
                tileGO.name = "Tile(" + i + ", " + j + ")";
                tileGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);
                SpriteRenderer tileSR = tileGO.AddComponent<SpriteRenderer>();

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

      
            // Wait for user to click end turn
            //game.nextTurn();
        }

        StartingMetrics();
        Camera.main.transform.position = new Vector3(game.Columns / 2, game.Rows / 2, -10);
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
        coinCount.text = coin.ToString();
        greenCount.text = green.ToString();
        happinessCount.text = happiness.ToString();
    }

    public void SetTurn(float turn)
    {
        currentTurn.text = turn.ToString();
    }
}
