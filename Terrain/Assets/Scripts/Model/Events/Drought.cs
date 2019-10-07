using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drought : Event
{
    private int turnToOccur;

    public Drought()
    {
        this.Type = EventType.Transition;
        this.Description = "Droughts everywhere are projected to become more intense, and summer temperatures are projected to continue rising.";
    }

    public Drought(int greenPointDelta, int happinessDelta, int moneyDelta)
    {
        this.GreenPointDelta = greenPointDelta;
        this.HappinessDelta = happinessDelta;
        this.MoneyDelta = moneyDelta;
        this.Type = EventType.Transition;
    }

    public int TurnToOccur { get => turnToOccur; set => turnToOccur = value; }

    public override void TitleDelta()
    {
        throw new System.NotImplementedException();
    }

}
