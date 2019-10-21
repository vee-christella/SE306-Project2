using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : Event
{
    private double probability;

    public Flood(Game game) : base(-100, -2, -50)
    {
        this.Type = EventType.TileChanger;
        this.Description = "Flooding means that excess water is destroying your city." +
            " Storms, high tides and rising sea levels increase the risk of flooding." +
            " Events like flooding have a higher chance of happening the lower your green points are" +
            "and the longer it takes you to reach 1000 green points!";
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
                if (tiles[i, j].Type == Tile.TileType.Desert ||  tiles[i,j].Type == Tile.TileType.Plain)
                {
                    Debug.Log("Found a tile with desert");
                    int random = Random.Range(0, 4);

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
