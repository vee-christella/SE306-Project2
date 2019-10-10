using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    // Initialise stats for each Factory building
    public Factory() : base(16, -100, 20, 0, 7, 8, 0, 3)
    {   
        this.TypeOfBuilding = BuildingType.Utility;
        this.Name = "Factory";
        this.Blurb = "A factory is a building used to process raw materials and manufacture goods.";
    }
}
