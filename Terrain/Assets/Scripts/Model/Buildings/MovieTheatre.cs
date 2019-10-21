using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieTheatre : Building
{
    // Initialise stats for each Movie Theatre building
    public MovieTheatre() : base(3, -50, 0, 3, 3, 0, 1, 5)
    {
        this.TypeOfBuilding = BuildingType.Recreational;
        this.Name = "Movie Theatre";
        this.Blurb = "A movie theatre is a place where people can gather and watch screenings of " +
            "movies at various times. For general public entertainment, cinema goers can purchase " +
            "tickets to watch films with their friends and family, however, the film industry contributes large amounts of waste to the environment.";
    }
}
