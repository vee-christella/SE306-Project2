using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsunami : Event
{

    private double probability;

    public Tsunami()
    {
        this.Type = EventType.Random;
    }

    public Tsunami(int greenPointDelta, int happinessDelta, int moneyDelta)
    {
        this.GreenPointDelta = greenPointDelta;
        this.HappinessDelta = happinessDelta;
        this.MoneyDelta = moneyDelta;
        this.Type = EventType.Random;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        throw new System.NotImplementedException();
    }
}
