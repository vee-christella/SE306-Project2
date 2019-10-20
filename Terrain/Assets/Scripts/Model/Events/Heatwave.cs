using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWave : Event
{
    private double probability;

    public HeatWave(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.TileChanger;
        this.Description = "Heat waves are periods of extremely hot weather lasting days to weeks. When soil moisture is low, heat waves become more prone to occurring. Due to drying up, your water tiles have turned into plains, and your plain tiles have turned into deserts.";
    }

    public double Probability { get => probability; set => probability = value; }


    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Plain)
                {
                    int random = Random.Range(0, 2);
                    // 50% chance to change plain tiles to desert
                    if (random == 1)
                    {
                        tiles[i, j].Type = Tile.TileType.Desert;
                    }
                }
            }
        }
    }
}
