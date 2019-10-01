using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : Building
{

    // Initialise stats for each Wind Turbine building
   public WindTurbine()
    {
        this.InitialMoney = -40;
        this.InitialGreen = 10;
        this.InitialHappiness = 2;

        this.GenerateGreen = 20;
        this.GenerateMoney = 0;
    }



}
