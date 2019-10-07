using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : Building
{
    // Initialise stats for each Zoo building
    public Zoo() : base(10, -150, 30, 40, 10, -60, 0, 6)
    {
        this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "Zoo";
        this.Blurb = "Zoos are a great source of fun and entertainment for families. Warmer temperatures alter habitats that are critical to some species, increasing the risk of localised extinction. Zoos are important they allow for the preservation" +
            " of endangered species and allow children to be educated about environmental sustainability of both flora and fauna.";
    }
}
