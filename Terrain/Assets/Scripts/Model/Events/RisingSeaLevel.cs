using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingSeaLevel : Event
{
    private double probability;

    public RisingSeaLevel(Game game) : base(-5, -1, -5)
    {
        this.Type = EventType.Random;
        this.Game = game;
        this.Description = "Global sea level is projected to rise another 1 to 4 feet by 2100. This is the result of added water from melting glaciers and the expansion of seawater as it warms. Rising sea levels will increase the risk of erosion, coastal flooding and saltwater intrusion, increasing the need for coastal protection";
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        return;
    }

}
