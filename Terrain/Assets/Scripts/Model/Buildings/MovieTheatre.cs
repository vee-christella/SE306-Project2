using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieTheatre : Building
{
    // Initialise stats for each Movie Theatre building
    public MovieTheatre() : base(-100, 0, 10, 0, 30, 0, 5)
    {
        this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "Movie Theatre";
        this.Blurb = "Your citizens will enjoy the latest movies with a " +
        	"movie theatre in the city!";
    }
}
