using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : Building
{
    // Initialise stats for each Race Track
    public RaceTrack() : base(7, -150, 0, 40, -10, 20, 0, 7)
    {
       this.TypeOfBuilding = BuildingType.Recreational;
    }
}
