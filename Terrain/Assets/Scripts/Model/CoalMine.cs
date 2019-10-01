using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMine : Building
{
    // Initialise stats for each Coal Mine building
    public CoalMine()
    {
        this.InitialMoney = -20;
        this.InitialGreen = -10;
        this.InitialHappiness = -10;

        this.GenerateGreen = -10;
        this.GenerateMoney = 50;
    }
}
