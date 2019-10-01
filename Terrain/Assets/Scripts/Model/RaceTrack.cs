using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : Building
{
    // Initialise stats for each Race Track
    public RaceTrack()
    {
        this.InitialBuildMoney = -150;
        this.InitialBuildGreen = 0;
        this.InitialBuildHappiness = 20;

        this.GenerateGreen = -10;
        this.GenerateMoney = 20;
    }
}
