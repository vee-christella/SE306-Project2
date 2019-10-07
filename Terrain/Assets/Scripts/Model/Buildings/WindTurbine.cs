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
            "the noise if they live close by. A wind turbine converts the wind’s kinetic energy into electrical energy and" +
            " is considered a sustainable form of electricity generation. These wind turbines are more efficient in certain" +
            " areas where the wind turbines do not need to incur large start up costs.";
    }



}
