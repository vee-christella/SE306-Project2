using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : Event
{
    private double probability;

    public Hurricane(Game game) :base (-5,-1,-5)
    {
        this.Type = EventType.Random;
        this.Game = game;
        this.Description = "The intensity, frequency and duration of the strongest hurricanes, have all increased since the early 1980s. Hurricane-associated storm intensity and rainfall rates are projected to increase as the climate continues to warm.";
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        return;
    }
}
