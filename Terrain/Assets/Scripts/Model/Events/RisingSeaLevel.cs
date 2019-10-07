using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingSeaLevel : Event
{
    private double probability;

    public RisingSeaLevel()
    {
        this.Type = EventType.Random;
        this.Description = "Global sea level is projected to rise another 1 to 4 feet by 2100. This is the result of added water from melting glaciers and the expansion of seawater as it warms. Rising sea levels will increase the risk of erosion, coastal flooding and saltwater intrusion, increasing the need for coastal protection";
    }

    public RisingSeaLevel(int greenPointDelta, int happinessDelta, int moneyDelta)
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
