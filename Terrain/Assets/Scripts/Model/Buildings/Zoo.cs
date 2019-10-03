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
        this.Blurb = "A zoo that houses different animals in a sustainable" +
        	"manner. Sustainability will also be taught to visitors, hence" +
        	"increasing environmental awareness";
    }
}
