using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMine : Building
{
    // Initialise stats for each Coal Mine building
    public CoalMine()
    {
        this.InitialBuildMoney = -20;
        this.InitialBuildGreen = -10;
        this.InitialBuildHappiness = -10;

        this.GenerateGreen = -10;
        this.GenerateMoney = 50;
    }
}
