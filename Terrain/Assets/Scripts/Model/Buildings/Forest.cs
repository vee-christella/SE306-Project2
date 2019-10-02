using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Building
{
    // Initialise stats for each Forest
    public Forest() : base(0, 50, 5, 0, 20, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.Misc;
        this.Name = "Forest";
        this.Blurb = "Forests are extremely green, and can house habitats for " +
            "animals. Build more forests to increase your green points!";
    }
}
