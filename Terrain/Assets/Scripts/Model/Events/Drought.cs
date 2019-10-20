using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drought : Event
{
    private int turnToOccur;
    public Drought(Game game) :base(-100,-2,-50)
    {
        this.Type = EventType.TileChanger;
        this.Description = "Droughts everywhere are projected to become more intense, and summer temperatures are projected to continue rising.";
        Game = game;
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
