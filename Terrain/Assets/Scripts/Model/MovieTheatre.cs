using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieTheatre : Building
{
    // Initialise stats for each Movie Theatre building
    public MovieTheatre()
    {
        this.InitialMoney = -100;
        this.InitialGreen = 0;
        this.InitialHappiness = 10;

        this.GenerateGreen = 0;
        this.GenerateMoney = 30;
    }
}
