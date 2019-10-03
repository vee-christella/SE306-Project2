using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : Building
{

    // Initialise stats for each Wind Turbine building
   public WindTurbine() :  base(9, -40, 10, 2, 20, 0, 0, 1)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
        this.Name = "Wind Turbine";
        this.Blurb = "Wind Turbines are a sustainable choice of energy" +
        	"generation, but are very loud. Your citizens may not appreciate" +
        	"the noise if they live close by.";
    }



}
