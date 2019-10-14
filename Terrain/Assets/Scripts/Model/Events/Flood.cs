using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : Event
{
    private double probability;

    public Flood(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.TileChanger;
        this.Description = "Storm surges and high tides combined with rising sea levels is increasing flooding in many regions. Furthermore, frequent intense rainfalls increases the likelihood of rivers flooding, and flash flooding when urban drainage systems become overwhelmed.";

        this.Game = game;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Desert)
                {
                    Debug.Log("Found a tile with desert");
                    int random = Random.Range(0, 2);

                    // 50% chance to change desert tile to water tile
                    if (random == 1)
                    {
                        tiles[i, j].Type = Tile.TileType.Water;
                    }
                }
            }
        }
    }

}
