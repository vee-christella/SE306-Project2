using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpawn : Event
{
    private double probability;

    public ForestSpawn()
    {
        this.Type = EventType.Random;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TitleDelta()
    {
        throw new System.NotImplementedException();
    }
}
