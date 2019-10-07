using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drought : Event
{
    private int turnToOccur;

    public Drought()
    {
        this.Type = EventType.Transition;
    }

    public Drought(int greenPointDelta, int happinessDelta, int moneyDelta)
    {
        this.GreenPointDelta = greenPointDelta;
        this.HappinessDelta = happinessDelta;
        this.MoneyDelta = moneyDelta;
        this.Type = EventType.Transition;
    }

    public int TurnToOccur { get => turnToOccur; set => turnToOccur = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        Debug.Log("tile delta called");

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Type == Tile.TileType.Water)
                {
                    Debug.Log("Found a tile with water");
                    int random = Random.Range(0, 2);
                    // 50% chance to change tiles to desert
                    if (random == 1) {
                        tiles[i, j].Type = Tile.TileType.Desert;
                    }
                }
            }
        }
    }

}
