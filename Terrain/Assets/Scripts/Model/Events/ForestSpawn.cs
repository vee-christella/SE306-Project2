using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpawn : Event
{
    private double probability;

    public ForestSpawn(Game game) : base(2, 2, 2)
    {
        this.Type = EventType.Random;
        this.Game = game;
    }


    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        int count = 0;

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Plain)
                {
                    int random = Random.Range(0, 9);
                    // 50% chance to change tiles to desert
                    if (random == 1)
                    {
                        Debug.Log("spawn forest ");
                        Game.addBuildingToTile("Forest", tiles[i,j]);
                        count++;
                    }
                }
            }
        }

        // refund the player for the amount it costs to  build all the forests as they should be free 
        Game.Money = Game.Money + (count * 5);
        Game.Green = Game.Green + (count * -5);
    }
}
