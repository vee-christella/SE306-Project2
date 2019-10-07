using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : Building
{
    // Initialise stats for each Zoo building
    public Zoo() : base(10, -100, 10, 10, -30, 10, 2, 6)
    {
        this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "Zoo";
        this.Blurb = "Zoos are a great source of fun and entertainment for families. They allow for the preservation" +
            " of endangered species and allow children to be educated about environmental sustainability of both flora and fauna.";
    }
}
