using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/*
Class to represent a game tile that makes up part of the game world. Players can build
buildings on tiles. Tiles have different types, which affect which buildings can be
built on top of them.
*/
public class Tile
{
    public enum TileType
    {
        Desert,
        Mountain,
        Plain,
        Water,
        Null
    };

    private List<string> waterBuildable = new List<string>() { "Hydro Plant", "Pollutant", null };
    private List<string> desertBuildable = new List<string>(){"National Park", "Pollutant", "Nuclear Plant", "Oil Refinery",
    "Race Track", "Solar Farm", "Factory", "Recycling Plant", null};
    private List<string> plainBuildable = new List<string>(){"Forest", "Pollutant", "Movie Theatre", "National Park", "Nuclear Plant",
    "Race Track", "Wind Turbine", "Zoo", "Town Hall", "Animal Farm", "Bee Farm", "Factory", "Greenhouse", "Recycling Plant",
    "Vegetable Farm", null};
    private List<string> mountainBuildable = new List<string>() { "Coal Mine", "Pollutant", null };

    Action<Tile> callbackTypeChanged;
    Action<Tile> callbackBuildingChange;
    TileType type;
    TileType oldType = TileType.Null;
    private int x;
    private int y;
    private int z;
    private Building building = null;
    private Game game;

    public TileType Type
    {
        get => type; set
        {
            oldType = type;
            type = value;
            game.StillBuildable(this);
            if (callbackTypeChanged != null)
            {
                Debug.Log("TIleTypeChanged");
                callbackTypeChanged(this);
            }
        }
    }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Z { get => z; set => z = value; }
    public Building Building { get => building; }
    public Action<Tile> CallbackBuildingChange { get => callbackBuildingChange; }
    public Action<Tile> CallbackTypeChanged { get => callbackTypeChanged; }

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

            // If in tutorial, check if the coal mine is being built to progress
            if (PlayerPrefs.GetInt("Level") == 0 && building.GetType().Name.ToString() == "CoalMine")
            {
                TutorialController.Instance.coalMineBuilt = true;
            }

            // Success
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
        if (this.building != null)
        {
            this.building = null;

            if (CallbackBuildingChange != null)
            {
                CallbackBuildingChange(this);
            }
            Debug.Log("Building removed");

            return true;
        }

        Debug.Log("No building to remove");
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
            if (!mountainBuildable.Contains(building.Name))
            {
                return false;
            }
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

    public bool wasBuildable(Building building)
    {
        if(oldType!= TileType.Null)
        {
            // No buildings can be built on mountain tiles
            if (this.oldType == Tile.TileType.Mountain)
            {
                if (!mountainBuildable.Contains(building.Name))
                {
                    return false;
                }
            }

            else if (this.oldType == Tile.TileType.Water)
            {
                if (!waterBuildable.Contains(building.Name))
                {
                    return false;
                }
            }

            else if (this.oldType == Tile.TileType.Desert)
            {
                if (!desertBuildable.Contains(building.Name))
                {
                    return false;
                }
            }

            else if (this.oldType == Tile.TileType.Plain)
            {
                if (!plainBuildable.Contains(building.Name))
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void registerMethodCallbackTypeChanged(Action<Tile> method)
    {
        callbackTypeChanged += method;
    }

    public void unregisterMethodCallbackTypeChanged()
    {
        callbackTypeChanged = null;
    }
}
