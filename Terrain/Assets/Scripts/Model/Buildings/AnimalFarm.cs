using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFarm : Building
{
    // Initialise stats for each Vegan Farm building
    public AnimalFarm() : base(17, -100, -10, 0, 12, -5, 0, 3)
    {   
        this.TypeOfBuilding = BuildingType.Utility;
        this.Name = "Animal Farm";
        this.Blurb = "An animal farm is a farm containing livestock such as cows, sheep and chickens which are raised for human consumption.";
    }
}
