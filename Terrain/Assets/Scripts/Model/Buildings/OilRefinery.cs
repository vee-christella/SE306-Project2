using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRefinery : Building
{
    // Initialise stats for each Oil Refinery building
    public OilRefinery() : base(-50, -80, -10, -20, 40, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
    }

    // TODO: Add chance of oil spill
    public bool CheckForSpill()
    {
        return false;
    }


}
