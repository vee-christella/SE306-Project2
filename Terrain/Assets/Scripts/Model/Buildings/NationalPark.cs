using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationalPark : Building
{
    // Initialise stats for each National Park building
    public NationalPark() : base(-100, 20, 30, 5, -10, 0, 5)
    {
       this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "National Park";
        this.Blurb = "National parks provide a fresh, clean and green " +
        	"environment for your citizens to enjoy for free. Plants can" +
        	"also be planted here, giving an increase in green points!";
    }
}
