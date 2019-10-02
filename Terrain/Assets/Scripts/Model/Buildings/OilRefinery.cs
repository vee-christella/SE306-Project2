using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRefinery : Building
{
    // Initialise stats for each Oil Refinery building
    public OilRefinery() : base(-50, -80, -10, -20, 40, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
        this.Name = "Oil Refinery";
        this.Blurb = "Oil Refineries can only be built on top of desert tiles," +
        	"as most of the oil are extracted underground. This is a good way" +
        	"to generate energy, but is dangerous to the environment. It can" +
        	"also cause a spill which will heavily decrease your green points.";
    }

    // TODO: Add chance of oil spill
    public bool CheckForSpill()
    {
        return false;
    }


}
