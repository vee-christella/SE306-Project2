using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydro : Building
{
    // Initialise stats for each Hydro building
    public Hydro()
    {
        this.InitialBuildMoney = -80;
        this.InitialBuildGreen = 20;
        this.InitialBuildHappiness = 5;

        this.GenerateGreen = 20;
        this.GenerateMoney = 10;
    }
}
