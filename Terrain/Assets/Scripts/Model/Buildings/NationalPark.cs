using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationalPark : Building
{
    // Initialise stats for each National Park building
    public NationalPark() : base(4, -150, 25, 5, -10, 10, 2, 5)
    {
       this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "National Park";
        this.Blurb = "A national park is where visitors can explore beautiful and natural scenery. " +
            "These parks are essential to the sustainability of the environment as they educate " +
            "visitors about the importance of environmental preservation while providing an enjoyable experience.";
    }
}
