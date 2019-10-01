using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRefinery : Building
{
    // Initialise stats for each Oil Refinery building
    public OilRefinery()
    {
        this.InitialBuildMoney = -50;
        this.InitialBuildGreen = -80
        this.InitialBuildHappiness = -10;

        this.GenerateGreen = -20;
        this.GenerateMoney = 40;
    }

    // TODO: Add chance of oil spill

}
