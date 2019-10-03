using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum TileType
    {
        Desert, Mountain, Plain, Water
    };

    TileType type;
    private int x;
    private int y;
    private int z;
    private Building building;
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
        this.building = building;
        return true;
    }

}
