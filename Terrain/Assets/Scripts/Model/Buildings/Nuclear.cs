using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : Building
{
    // Initialise stats for each Nuclear plant building
    public Nuclear() : base(-60, 0, -20, 0, 50, 0)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
    }

    // TODO: Add chance of nuclear disaster
}
