using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : Event
{
    private double probability;

    public Earthquake(Game game) :base (-5,-1,-5)
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
