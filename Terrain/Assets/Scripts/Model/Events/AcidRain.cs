using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRain : Event
{

    private double probability;

    public AcidRain(Game game) :base (-100,-5,-50)
    {
        this.Type = EventType.BuildingDestroyer;
        this.Description = "Acid rain contains lots of acid and is very destructive for buildings and the environment. " +
            "They come from the bad effects of coal-burning power plants, factories, and cars. " +
            "Events like acid rain have a higher chance of happening the lower your green points are" +
            "and the longer it takes you to reach 1000 green points!" ;
        this.Game = game;
        this.DestroysBuildings = true;
        this.Title = "Acid Rain";
    }

    public double Probability { get => probability; set => probability = value; }

 
    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {

        if (doDestroyBuildings)
        {
            DestroyBuildings();
        }

    }

}
