using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydro : Building
{
    // Initialise stats for each Hydro building
    public Hydro() : base(1, -80, 20, 5, 20, 10, 0, 4)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
        this.Name = "Hydro Plant";
        this.Blurb = "A hydroelectric power station uses turbines to generate electricity. These hydro plants consume no water, " +
            "only using the kinetic energy from the water's tidal waves to generate sustainable energy.";
    }
}
