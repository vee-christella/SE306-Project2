using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFire : Event
{

    private double probability;

    public WildFire()
    {
        this.Type = EventType.Random;
    }

    public WildFire(int greenPointDelta, int happinessDelta, int moneyDelta)
    {
        this.GreenPointDelta = greenPointDelta;
        this.HappinessDelta = happinessDelta;
    }
    
    public override void TitleDelta()
    {
        throw new System.NotImplementedException();
    }

}