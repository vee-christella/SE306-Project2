using System.Collections.Generic;
using UnityEngine;

public class Greenhouse : Building
{
    public Greenhouse() : base(8, -100, -10, 0, 4, 0, 0, 3)
    {
        this.Name = "Greenhouse";
        this.TypeOfBuilding = BuildingType.Utility;
        this.Blurb = "Greenhouses are low carbon structures that are used to grow crops in a controlled environment. " +
            "Growing plants in a greenhouse allows for the growing season to be extended, particularly for plants " +
            "that do not thrive in winter. Recent technological changes have allowed for greenhouses to reduce carbon emissions by 75%.";
    }

}
