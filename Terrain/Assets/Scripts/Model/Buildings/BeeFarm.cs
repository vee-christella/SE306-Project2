using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFarm : Building
{
    // Initialise stats for each Bee Farm building

    public BeeFarm() : base(14, -10, 5, 5, 3, 8, 0, 3)
    {   
        this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "Bee Farm";
        this.Blurb = "A bee farm contains bee hives and is a centre for honey production. Pesticides and disease are destroying the world bee population at an alarming rate. Apart from the decline in honey production, so is the pollination of major crops.";
    }
}
