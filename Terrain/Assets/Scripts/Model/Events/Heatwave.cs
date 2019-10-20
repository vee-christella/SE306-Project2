using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWave : Event
{
    private double probability;

    public HeatWave(Game game) : base(-50, -5, 0)
    {
        this.Type = EventType.TileChanger;
        this.Description = "Heat waves are periods of abnormally hot weather lasting days to weeks. This is combined with a reduction of soil moisture which exacerbates heat waves.";
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
