using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : Event
{
    private double probability;

    public Hurricane()
    {
        this.Type = EventType.Random;
        this.Description = "The intensity, frequency and duration of the strongest hurricanes, have all increased since the early 1980s. Hurricane-associated storm intensity and rainfall rates are projected to increase as the climate continues to warm.";
    }

    public Hurricane(int greenPointDelta, int happinessDelta, int moneyDelta)
    {
        this.GreenPointDelta = greenPointDelta;
        this.HappinessDelta = happinessDelta;
        this.MoneyDelta = moneyDelta;
        this.Type = EventType.Random;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TitleDelta()
    {
        throw new System.NotImplementedException();
    }
}