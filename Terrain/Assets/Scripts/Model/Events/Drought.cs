using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drought : Event
{
    private int turnToOccur;
    public Drought(Game game) :base(-5,-1,-5)
    {
        this.Type = EventType.TileChanger;
        this.Description = "Droughts occur when there is less rain than expected, which means that your town is running out of water! Your water supply is lacking and some of your water tiles have dried up into desert tiles. All your buildings that are built on dried up water tiles have been disabled.";
        Game = game;
        this.Title = "Drought";
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
                    //Debug.Log("Found a tile with water");
                    int random = Random.Range(0, 2);
                    // 50% chance to change tiles to desert
                    if (random == 1)
                    {
                        tiles[i, j].Type = Tile.TileType.Desert;
                    }
                }
            }
        }
    }

}
