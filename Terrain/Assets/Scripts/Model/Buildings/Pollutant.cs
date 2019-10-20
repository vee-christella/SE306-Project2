using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollutant : Building
{
    // Initialise stats for each Zoo building
    public Pollutant() : base(10, 2000, 0, 0, -15, -15, 0, 6)
    {
        this.Name = "Pollutant";
    }
}
