using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : Event
{

    public EarthQuake(Game game):base(-20,-5,-50)
    {
        this.Type = EventType.TileChanger;
        this.Game = game;
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