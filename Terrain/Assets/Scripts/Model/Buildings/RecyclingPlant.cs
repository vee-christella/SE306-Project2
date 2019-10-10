using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingPlant : Building
{
    // Initialise stats for each Recycling Plant building

    public RecyclingPlant() : base(13, -50, 20, 0, 7, 8, 0, 3)
    {   
        this.TypeOfBuilding = BuildingType.Utility;
        this.Name = "Recycling Plant";
        this.Blurb = "Community recycling plants are facilities where citizens can drop off unwanted items and materials for reuse and recycling. The aim of these buildings is to reduce waste to landfill by reusing, re-purposing and recycling as much as possible.";
    }
}
