using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationalPark : Building
{
    // Initialise stats for each National Park building
    public NationalPark()
    {
        this.InitialMoney = -100;
        this.InitialGreen = 20;
        this.InitialHappiness = 30;

        this.GenerateGreen = 5;
        this.GenerateMoney = -10;
    }
}
