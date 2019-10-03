using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : Building
{
    // Initialise stats for each Nuclear plant building
    public Nuclear() : base(5, -60, 0, -20, 0, 50, 0, 4)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
        this.Name = "Nuclear Plant";
        this.Blurb = "Nuclear Plants are good for energy generation, but" +
        	" it has a very low chance of a disaster. Since" +
        	"nuclear is a very dangerous substance, be careful in building these!" +
        	"If a nuclear disaster occurs, your city will be destroyed.";
    }

    // TODO: Add chance of nuclear disaster
}
