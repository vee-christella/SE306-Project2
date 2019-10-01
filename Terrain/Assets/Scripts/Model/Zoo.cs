using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : Building
{
    // Initialise stats for each Zoo building
    public Zoo() { 
    
        this.InitialBuildMoney = -150;
        this.InitialBuildGreen = 30;
        this.InitialBuildHappiness = 40;

        this.GenerateGreen = 10;
        this.GenerateMoney = -60;
    }
}
