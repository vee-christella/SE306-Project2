using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rejuvenation : Event
{

    public Rejuvenation(Game game):base(50,5,0)
    {
        this.Type = EventType.Good;
        this.Game = game;
        this.Title = "Rejuvenation";
        this.Description = "Your abundance in green points has caused triggered an event that causes the earth to heal. " +
            "Events like rejuvenation have a higher chance of happening the more green points you have.";
        this.TileDeltaDesc = "Some desert tiles have been turned to fertile plain tiles.";
    }

    public override void TileDelta(Tile[,] tiles, bool doDestoryBuildings)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Desert)
                {
                    int random = Random.Range(0, 2);

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
