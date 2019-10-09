using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildfire : Event
{
    private double probability;

    public Wildfire(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.Random;
        this.Game = game;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        return;
    }
}
