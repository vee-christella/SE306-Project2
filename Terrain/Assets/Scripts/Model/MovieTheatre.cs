using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieTheatre : Building
{
    // Initialise stats for each Movie Theatre building
    public MovieTheatre()
    {
        this.InitialBuildMoney = -100;
        this.InitialBuildGreen = 0;
        this.InitialBuildHappiness = 10;

        this.GenerateGreen = 0;
        this.GenerateMoney = 30;
    }
}
