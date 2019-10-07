using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWave : Event
{
    private double probability;

    public HeatWave()
    {
        this.Type = EventType.Random;
        this.Description = "Heat waves are periods of abnormally hot weather lasting days to weeks. This is combined with a reduction of soil moisture which exacerbates heat waves.";
    }

    public HeatWave(int greenPointDelta, int happinessDelta, int moneyDelta)
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
