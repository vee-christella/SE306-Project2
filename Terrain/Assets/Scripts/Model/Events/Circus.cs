using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circus : Event
{
    public Circus(Game game) : base(0, 5, 0)
    {
        this.Type = EventType.Good;
        this.Description = "The Circus is in Town! /n The people of your town feel extremely happy about this";
        this.Game = game;
    }

    public override float CalculateCostToRepair(Tile[,] tiles)
    {
        return 0;
    }

    public override void TileDelta(Tile[,] tiles, bool doDestoryBuildings)
    {
        return;
    }







}
