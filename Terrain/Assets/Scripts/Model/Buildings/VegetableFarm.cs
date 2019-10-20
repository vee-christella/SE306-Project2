using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableFarm : Building
{
    // Initialise stats for each Vegetable Farm building

    
    public VegetableFarm() : base(15, -70, 0, 0, 4, -2, 0, 3)
    {   
        this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "Vegetable Farm";
        this.Blurb = "A vegetable farm is the growing of fresh vegetable crops for human consumption such as potatoes and sweetcorn.";
    }
}
