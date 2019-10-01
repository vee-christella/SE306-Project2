using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydro : Building
{
    // Initialise stats for each Hydro building
    public Hydro()
    {
        this.InitialMoney = -80;
        this.InitialGreen = 20;
        this.InitialHappiness = 5;

        this.GenerateGreen = 20;
        this.GenerateMoney = 10;
    }
}
