using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : Building
{
    // Initialise stats for each Coal Mine building
    public TownHall() : base(11, 0, 0, 0, 0, 0, 0, 0)
    {
        this.TypeOfBuilding = BuildingType.Misc;
        this.Name = "Town Hall";
        this.Blurb = "The Town Hall is the starting building of the game.";
    }
}
