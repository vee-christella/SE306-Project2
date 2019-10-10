using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : Event
{
    private double probability;

    public Flood(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.Random;
        this.Description = "Storm surges and high tides combined with rising sea levels is increasing flooding in many regions. Furthermore, frequent intense rainfalls increases the likelihood of rivers flooding, and flash flooding when urban drainage systems become overwhelmed.";

        this.Game = game;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        return;
    }

}
