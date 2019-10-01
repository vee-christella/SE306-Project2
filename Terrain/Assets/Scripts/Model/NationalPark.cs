using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationalPark : Building
{
    // Initialise stats for each National Park building
    public NationalPark()
    {
        this.InitialBuildMoney = -100;
        this.InitialBuildGreen = 20;
        this.InitialBuildHappiness = 30;

        this.GenerateGreen = 5;
        this.GenerateMoney = -10;
    }
}
