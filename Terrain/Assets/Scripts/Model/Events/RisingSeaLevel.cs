using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingSeaLevel : Event
{
    private double probability;

    public RisingSeaLevel(Game game) : base(-150, -5, 0)
    {
        this.Type = EventType.TileChanger;
        this.Game = game;
        this.Description = "Global sea level is projected to rise another 1 to 4 feet by 2100. This is the result of added water from melting glaciers and the expansion of seawater as it warms. Rising sea levels will increase the risk of erosion, coastal flooding and saltwater intrusion, increasing the need for coastal protection";
    }

    public double Probability { get => probability; set => probability = value; }



    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (i == 0)
                {
                    tiles[i, j].Type = Tile.TileType.Water;
                }
                else if (i == tiles.GetLength(0) - 1)
                {
                    tiles[i, j].Type = Tile.TileType.Water;
                }

                else if (j == 0)
                {
                    tiles[i, j].Type = Tile.TileType.Water;
                }
                else if (j == tiles.GetLength(1) - 1)
                {
                    tiles[i, j].Type = Tile.TileType.Water;
                }
            }
        }
    }

}
