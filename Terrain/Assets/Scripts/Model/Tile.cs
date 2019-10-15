using System.Collections;
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

    private List<string> waterBuildable = new List<string>() { "Hydro Plant", null };
    private List<string> desertBuildable = new List<string>(){"Coal Mine", "National Park", "Nuclear Plant", "Oil Refinery",
    "Race Track", "Wind Turbine", "Solar Farm", null};
    private List<string> plainBuildable = new List<string>(){"Coal Mine", "Forest", "Movie Theatre", "National Park", "Nuclear Plant",
    "Race Track", "Wind Turbine", "Solar Farm", "Zoo", "Town Hall", null};

    Action<Tile> callbackTypeChanged;
    Action<Tile> callbackBuildingChange;
    TileType type;
    private int x;
    private int y;
    private int z;
    private Building building = null;
    private Game game;

    public TileType Type
    {
        get => type; set
        {
            type = value;
            if (callbackTypeChanged != null)
            {
                callbackTypeChanged(this);
            }
        }
    }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Z { get => z; set => z = value; }
    public Building Building { get => building; }
    public Action<Tile> CallbackBuildingChange { get => callbackBuildingChange; }

    public Tile(Game game, TileType type, int x, int y, int z)
    {
        this.game = game;
        this.type = type;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Tile(Game game, int x, int z)
    {
        this.game = game;
        this.x = x;
        this.y = 0;
        this.z = z;
    }

    public void setType(TileType type)
    {
        Type = type;
    }
    public bool placeBuilding(Building building)
    {
        Debug.Log("Building Created");
        
        if ((this.building == null) && IsBuildable(building))
        {
            this.building = building;

            if (CallbackBuildingChange != null)
            {
                CallbackBuildingChange(this);
            }

            return true;
        }

        return false;
    }

    public bool removeBuilding()
    {
        //Debug.Log("Building Removed");
        if (this.building != null)
        {
            this.building = null;

            if (CallbackBuildingChange != null)
            {
                CallbackBuildingChange(this);
            }

            return true;
        }
        return false;
    }

    public void registerMethodCallbackBuildingCreated(Action<Tile> method)
    {
        callbackBuildingChange += method;
    }

    public void unregisterMethodCallbackBuildingCreated(Action<Tile> method)
    {
        callbackBuildingChange -= method;
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

    public void registerMethodCallbackTypeChanged(Action<Tile> method)
    {
        callbackTypeChanged += method;
    }
}
