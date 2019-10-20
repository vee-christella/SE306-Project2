﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rejuevnation : Event
{

    public Rejuevnation(Game game):base(50,5,0)
    {
        this.Type = EventType.Good;
        this.Game = game;
    }

    public override void TileDelta(Tile[,] tiles, bool doDestoryBuildings)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Desert)
                {
                    int random = Random.Range(0, 1);

                    // 50% chance to change desert tile to water tile
                    if (random == 1)
                    {
                        tiles[i, j].Type = Tile.TileType.Plain;
                    }
                }
            }
        }
    }
}
