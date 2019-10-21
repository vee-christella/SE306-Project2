using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circus : Event
{
    public Circus(Game game) : base(0, 10, 50)
    {
        this.Type = EventType.Good;
        this.Description = "The Circus is in town! The people of your town feel extremely happy about this.";
        this.Game = game;
        this.Title = "Circus";
        this.TileDeltaDesc = "Your population's happiness has increased.";
    }


    public override void TileDelta(Tile[,] tiles, bool doDestoryBuildings)
    {
        return;
    }







}
