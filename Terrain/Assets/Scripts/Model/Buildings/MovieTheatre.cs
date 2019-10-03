using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieTheatre : Building
{
    // Initialise stats for each Movie Theatre building
    public MovieTheatre() : base(3, -100, 0, 10, 0, 30, 0, 5)
    {
        this.TypeOfBuilding = BuildingType.Recreational;
    }
}
