using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildfire : Event
{
    private double probability;

    public Wildfire(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.Random;
        this.Description = "Severe heat and drought fuel wildfires, conditions scientists have linked to climate change. The hotter weather makes forests drier and more susceptible to burning with the average wildfire season three and a half months longer than it was a few decades back.";
        this.Game = game;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        return;
    }
}
