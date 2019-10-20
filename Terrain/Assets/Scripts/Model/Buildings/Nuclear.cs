using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : Building
{
    // Initialise stats for each Nuclear plant building
    public Nuclear() : base(5, -150, -25, -10, 10, -2, -4, 4)
    {
        this.TypeOfBuilding = BuildingType.EnergySource;
        this.Name = "Nuclear Plant";
        this.Blurb = "A nuclear power plant is a thermal power station where the heat source is a nuclear reactor. " +
            "The heat is used to generate electricity, as per typical thermal power stations. Nuclear power can " +
            "generate electricity without greenhouse gas emissions, however there is a risk of a nuclear meltdown " +
            "which can release deadly radiation into the environment. As of 2019, the world has 440 operating nuclear reactors.";
    }

    // TODO: Add chance of nuclear disaster
}
