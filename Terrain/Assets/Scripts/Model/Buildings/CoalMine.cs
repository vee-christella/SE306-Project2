using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMine : Building
{
    // Initialise stats for each Coal Mine building
    public CoalMine() : base(20, -100, -10, 0, 17, -10, 0, 3)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
        this.Name = "Coal Mine";
        this.Blurb = "Coal mines are used to extract coal from the ground to be used as fuel." +
            "This coal is transported from the mine to refineries which can then use coal for electricity generation" +
            "due to its high energy content. Coal mining is a dangerous activity and air pollution from coal-fired power" +
            " plants can lead to negative events such as smog and acid rain.";
    }
}
