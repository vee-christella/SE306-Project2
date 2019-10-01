using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : Building
{

    // Initialise stats for each Wind Turbine building
   public WindTurbine()
    {
        this.InitialBuildMoney = -40;
        this.InitialBuildGreen = 10;
        this.InitialBuildHappiness = 2;

        this.GenerateGreen = 20;
        this.GenerateMoney = 0;
    }



}
