using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : Building
{
    // Initialise stats for each Zoo building
    public Zoo() : base(-150, 30, 40, 10, -60, 0, 6)
    {
        this.TypeOfBuilding = BuildingType.Recreational;
    }
}
