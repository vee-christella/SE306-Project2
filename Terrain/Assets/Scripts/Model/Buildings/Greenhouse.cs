using System.Collections.Generic;
using UnityEngine;

public class Greenhouse : Building
{
    public Greenhouse() : base(8, -80, 30, 0, 20, -10, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.Utility;
        this.Name = "Greenhouse";
        this.Blurb = "Greenhouses are low carbon structures that are used to grow crops in a controlled environment. " +
            "Growing plants in a greenhouse allows for the growing season to be extended, particularly for plants " +
            "that do not thrive in winter. Recent technological changes have allowed for greenhouses to reduce carbon emissions by 75%.";
    }

}
