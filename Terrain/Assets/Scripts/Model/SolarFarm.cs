using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFarm : Building
{
    // Initialise stats for each Solar Farm building
    public SolarFarm()
    {
        this.InitialBuildMoney = -80;
        this.InitialBuildGreen = 30;
        this.InitialBuildHappiness = 0;

        this.GenerateGreen = 20;
        this.GenerateMoney = -10;
    }
}
