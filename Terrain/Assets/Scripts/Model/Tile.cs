﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tile
{
    public enum TileType
    {
        Desert,
        Mountain,
        Plain,
        Water
    };

    private List<string> waterBuildable = new List<string>(){"Hydro Plant"};
    private List<string> desertBuildable = new List<string>(){"Coal Mine", "National Park", "Nuclear Plant", "Oil Refinery",
    "Race Track", "Wind Turbine", "Solar Farm"};
    private List<string> plainBuildable = new List<string>(){"Coal Mine", "Forest", "Movie Theatre", "National Park", "Nuclear Plant",
    "Race Track", "Wind Turbine", "Solar Farm", "Zoo"};

    TileType type;
    private int x;
    private int y;
    private int z;
    private Building building = null;
    private Game game;

    public TileType Type { get => type; set => type = value; }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Z { get => z; set => z = value; }
    public Building Building { get => building; }

    public Tile(Game game, TileType type, int x, int y, int z)
    {
        this.game = game;
        this.type = type;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Tile(Game game, int x, int y)
    {
        this.game = game;
        this.x = x;
        this.y = y;
        this.z = 0;
    }

    public void setType(TileType type)
    {
        Type = type;
    }
    public bool placeBuilding(Building building)
    {
        if (this.building == null){
            if (IsBuildable(building)){
                this.building = building;
                return true;
            }
        }
        return false;
    }
    public bool IsBuildable(Building building)
    {
        // No buildings can be built on mountain tiles
        if (this.type == Tile.TileType.Mountain)
        {
            return false;
        }

        else if (this.type == Tile.TileType.Water)
        {
            if (!waterBuildable.Contains(building.Name))
            {
                return false;
            }
        } 
        
        else if (this.type == Tile.TileType.Desert)
        {
            if (!desertBuildable.Contains(building.Name))
            {
                return false;
            }
        }
        
        else if (this.type == Tile.TileType.Plain)
        {
            if (!plainBuildable.Contains(building.Name))
            {
                return false;
            }
        }

        return true;
    }

}