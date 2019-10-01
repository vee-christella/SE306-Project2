using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : Building
{
    // Initialise stats for each Nuclear plant building
    public Nuclear()
    {
        this.InitialBuildMoney = -60;
        this.InitialBuildGreen = 0;
        this.InitialBuildHappiness = -20;

        this.GenerateGreen = 0;
        this.GenerateMoney = 50;
    }

    // TODO: Add chance of nuclear disaster
}
