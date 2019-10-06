using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFarm : Building
{
    // Initialise stats for each Solar Farm building
    public SolarFarm() : base(8, -80, 30, 0, 20, -10, 0, 3)
    {   
        this.TypeOfBuilding = BuildingType.EnergySource;
        this.Name = "Solar Farm";
        this.Blurb = "A solar farm comprises of a large amount of solar panels and can generate electricity" +
            " from capturing solar energy. This is advantageous as this is a low cost form of renewable energy" +
            " generation, however it is weather dependent and utilises a large amount of land.";
    }
}
