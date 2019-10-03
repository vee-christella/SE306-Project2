using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Building
{
    // Initialise stats for each Forest
    public Forest() : base(2, 0, 50, 5, 0, 20, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.Misc;
    }
}
