using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWave : Event
{
    private double probability;

    public HeatWave(Game game) : base(-50, -5, 0)
    {
        this.Type = EventType.TileChanger;
        this.Title = "Heat Wave";
        this.Description = "Heat waves are periods of extremely hot weather lasting days to weeks. " +
            "When soil moisture is low, heat waves become more prone to occurring. " + "Events like acid rain have a higher chance of happening the lower your green points are " +
            "and the longer it takes you to reach 1000 green points!";
        this.TileDeltaDesc = "Due to drying up, your water tiles have turned into plains, and your plain tiles have turned into deserts.";
    }

    public double Probability { get => probability; set => probability = value; }


    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {

                if (tiles[i, j].Type == Tile.TileType.Water)
                {
                    int random = Random.Range(0, 4);
                    if (random == 1)
                    {
                        tiles[i, j].Type = Tile.TileType.Plain;
                    }
                    continue;
                }

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
