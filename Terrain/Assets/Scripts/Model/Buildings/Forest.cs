using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Building
{
    // Initialise stats for each Forest
    public Forest() : base(2, 0, 50, 5, 0, 20, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.Misc;
        this.Name = "Forest";
        this.Blurb = "Forests are an ecosystem that comprise of trees, plants and other flora and fauna. " +
            "They are a natural habitat for many species and are essential for the world’s ecosystem " +
            "as they ensure biodiversity, affect rainfall and offset carbon emissions.";
    }
}
