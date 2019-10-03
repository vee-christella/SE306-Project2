using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMine : Building
{
    // Initialise stats for each Coal Mine building
    public CoalMine() : base(0, -20, -10, -10, -10, 50, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
    }
}
