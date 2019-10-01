using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : Building
{
    // Initialise stats for each Race Track
    public RaceTrack()
    {
        this.InitialMoney = -150;
        this.InitialGreen = 0;
        this.InitialHappiness = 20;

        this.GenerateGreen = -10;
        this.GenerateMoney = 20;
    }
}
