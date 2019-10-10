using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRain : Event
{

    private double probability;

    public AcidRain(Game game) :base (-5,-1,-5)
    {
        this.Type = EventType.Random;
        this.Description = "Acid rain contains high levels of nitric and sulfuric acids and comes in the form of snow, fog, and tiny bits of dry material that settles to Earth. The biggest sources are coal-burning power plants, factories, and automobiles. When humans burn fossil fuels, sulfur dioxide (SO2) and nitrogen oxides (NOx) are released into the atmosphere and when it reaches Earth, it flows across the surface in runoff water, enters water systems, and sinks into the soil";
        this.Game = game;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles)
    {
        return;
    }

}
