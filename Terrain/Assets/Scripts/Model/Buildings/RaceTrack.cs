using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : Building
{
    // Initialise stats for each Race Track
    public RaceTrack() : base(7, -100, -50, 5, -4, -4, 3, 7)
    {
       this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "Race Track";
        this.Blurb = "A race track is where townspeople can gather for karting activities as a form of entertainment. " +
            "Although a fun sport, racing causes a negative impact on the environment due to unnecessary consumption of fuel.";
    }
}
