using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWave : Event
{
    private double probability;

    public HeatWave(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.Random;
        this.Description = "Heat waves are periods of abnormally hot weather lasting days to weeks. This is combined with a reduction of soil moisture which exacerbates heat waves.";
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        throw new System.NotImplementedException();
    }
}
