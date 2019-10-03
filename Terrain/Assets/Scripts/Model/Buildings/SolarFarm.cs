using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFarm : Building
{
    // Initialise stats for each Solar Farm building
    public SolarFarm() : base(8, -80, 30, 0, 20, -10, 0, 3)
    {   
        this.TypeOfBuilding = BuildingType.EnergySource;
    }
}
