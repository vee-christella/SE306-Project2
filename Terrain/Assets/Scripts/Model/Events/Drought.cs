﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drought : Event
{
    private int turnToOccur;
    public Drought(Game game) :base(-100,-2,-50)
    {
        this.Type = EventType.TileChanger;
        this.Description = "Droughts occur when there is less rain than expected, which means that your town is running out of water! " +
            "Your water supply is lacking and some of your water tiles have dried up into desert tiles. " 
            + "Events like droughts have a higher chance of happening the lower your green points are " +
            "and the longer it takes you to reach 1000 green points!";
        Game = game;
        this.Title = "Drought";
        this.TileDeltaDesc = "All your buildings that are built on dried up water tiles have been disabled.";
    }

    public int TurnToOccur { get => turnToOccur; set => turnToOccur = value; }



    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        //Debug.Log("tile delta called");

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Water)
                {
                    int random = Random.Range(0, 4);
                    // 50% chance to change tiles to desert
                    if (random == 1)
                    {
                        tiles[i, j].Type = Tile.TileType.Desert;
                    }
                    else if (random == 0)
                    {
                        tiles[i, j].Type = Tile.TileType.Plain;
                    }
                }
            }
        }
    }

}
