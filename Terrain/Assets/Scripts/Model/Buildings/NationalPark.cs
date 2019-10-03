using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationalPark : Building
{
    // Initialise stats for each National Park building
    public NationalPark() : base(4, -100, 20, 30, 5, -10, 0, 5)
    {
       this.TypeOfBuilding = BuildingType.Recreational;
    }
}
