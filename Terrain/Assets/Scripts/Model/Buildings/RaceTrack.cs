﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : Building
{
    // Initialise stats for each Race Track
    public RaceTrack() : base(7, -150, 0, 40, -10, 20, 0, 7)
    {
       this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "Race Track";
        this.Blurb = "Race Tracks will encourage your citizens to engage in" +
        	"petrol racing events! This will increase your citizen's happiness, " +
        	"but due to more petrol being used, is ultimately going to " +
        	"negatively impact the environment.";
    }
}
