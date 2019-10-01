using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFarm : Building
{
    // Initialise stats for each Solar Farm building
    public SolarFarm()
    {
        this.InitialMoney = -80;
        this.InitialGreen = 30;
        this.InitialHappiness = 0;

        this.GenerateGreen = 20;
        this.GenerateMoney = -10;
    }
}
