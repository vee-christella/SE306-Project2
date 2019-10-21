using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : Event
{
    private double probability;

    public Flood(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.TileChanger;
        this.Description = "Flooding means that excess water is destroying your city. Storms, high tides and rising sea levels increase the risk of flooding. Flooding can also occur from rivers not being able to hold the amount of water they receive, as well as flash flooding which occurs randomly when heavy rain occurs and your town's drainage is too weak.";
        this.Title = "Flood";
        this.Game = game;
        this.TileDeltaDesc = "Some of your plains and desert tiles have turned to water tiles.";
    }

    public double Probability { get => probability; set => probability = value; }



    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Desert)
                {
                    Debug.Log("Found a tile with desert");
                    int random = Random.Range(0, 2);

                    // 50% chance to change desert tile to water tile
                    if (random == 1)
                    {
                        tiles[i, j].Type = Tile.TileType.Water;
                    }
                }
            }
        }
    }

}
