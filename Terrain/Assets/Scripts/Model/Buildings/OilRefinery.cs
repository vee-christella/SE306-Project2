using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRefinery : Building
{
    // Initialise stats for each Oil Refinery building
    public OilRefinery() : base(6, -210, -50, -3, 15, -8, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
        this.Name = "Oil Refinery";
        this.Blurb = "An oil refinery is an industrial process plant where crude oil is " +
            "transformed and refined into other products such as petroleum. This " +
            "petroleum is used as fuel in other processes as well as transportation.";
    }

    // TODO: Add chance of oil spill
    public bool CheckForSpill()
    {
        return false;
    }


}
