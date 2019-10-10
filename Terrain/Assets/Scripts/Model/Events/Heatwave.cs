using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatwave : Event
{
    private double probability;

    public Heatwave(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.Random;
        this.Description = "Rising temperatures are affecting wildlife and their habitats. As temperatures change, many species are on the move. Some butterflies, foxes, and alpine plants have migrated farther north or to higher, cooler areas.";
        this.Game = game;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        return;
    }
}
