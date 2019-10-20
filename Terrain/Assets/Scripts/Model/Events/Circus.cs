﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circus : Event
{
    public Circus(Game game) : base(0, 5, 0)
    {
        this.Type = EventType.Good;
        this.Description = "The Circus is in town! /n The people of your town feel extremely happy about this.";
        this.Game = game;
    }


    public override void TileDelta(Tile[,] tiles, bool doDestoryBuildings)
    {
        return;
    }







}
