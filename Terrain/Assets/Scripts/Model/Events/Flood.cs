using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : Event
{

    private double probability;

    public Flood()
    {
        this.Type = EventType.Random;
        this.Description = "Storm surges and high tides combined with rising sea levels is increasing flooding in many regions. Furthermore, frequent intense rainfalls increases the likelihood of rivers flooding, and flash flooding when urban drainage systems become overwhelmed.";
    }

    public Flood(int greenPointDelta, int happinessDelta, int moneyDelta)
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
