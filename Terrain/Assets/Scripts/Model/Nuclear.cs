using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : Building
{
    // Initialise stats for each Nuclear plant building
    public Nuclear()
    {
        this.InitialMoney = -60;
        this.InitialGreen = 0;
        this.InitialHappiness = -20;

        this.GenerateGreen = 0;
        this.GenerateMoney = 50;
    }

    // TODO: Add chance of nuclear disaster
}
