using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    // Initialise stats for each Factory building
    public Factory() : base(16, -200, -50, -5, 25, -7, -2, 3)
    {   
        this.TypeOfBuilding = BuildingType.Utility;
        this.Name = "Factory";
        this.Blurb = "A factory is a building used to process raw materials and manufacture goods.";
    }
}
