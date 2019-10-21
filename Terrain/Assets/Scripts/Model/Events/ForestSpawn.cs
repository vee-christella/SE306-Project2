using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpawn : Event
{
    private double probability;

    public ForestSpawn(Game game) : base(20, 2, 0)
    {
        this.Type = EventType.Good;
        this.Description = "Seedlings from trees, flowers and plants have been blown to different parts of the map, starting more plant growth. " +
            "Good events like forests spawning have a higher chance of happening the more green points you have!";
        this.Game = game;
        this.Title = "Forest Spawn";
        this.TileDeltaDesc = "More forests have naturally grown in your town!";
    }

    public double Probability { get => probability; set => probability = value; }


    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        int count = 0;

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Plain)
                {
                    int random = Random.Range(0,20);
                    // 50% chance to change tiles to desert
                    if (random <= 1)
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
