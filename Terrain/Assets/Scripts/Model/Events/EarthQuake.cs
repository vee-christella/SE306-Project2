using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : Event
{

    public EarthQuake(Game game):base(-20,-5,-50)
    {
        this.Type = EventType.TileChanger;
        this.Game = game;
        this.Description = "Earth quakes happen when two large pieces of the Earth's crust suddenly slip. " +
            "This causes shock waves to shake the surface of the Earth in the form of an earthquake. " +
            "Thankfully, an earthquake of this magnitude won't happen again in the foreseeable future.";
        this.Title = "Earthquake";
        this.TileDeltaDesc = "Some of your mountains have turned into plain tiles.";
    }

    public override void TileDelta(Tile[,] tiles, bool doDestoryBuildings)
    {

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Mountain)
                {
                    //Debug.Log("Found a tile with water");
                    int random = Random.Range(0, 5);
                    // 50% chance to change tiles to desert
                    if (random == 1)
                    {
                        tiles[i, j].Type = Tile.TileType.Plain;
                    }
                }
            }
        }
    }
}