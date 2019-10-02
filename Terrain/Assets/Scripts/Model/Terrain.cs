using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain
{
    public enum TerrainType { Desert, Mountain, Plain, Water };

    TerrainType type;
    private int x;
    private int y;
    private int z;
    private Building building;

    public TerrainType Type { get => type; set => type = value; }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Z { get => z; set => z = value; }
    public Building Building { get => building; set => building = value; }

    public Terrain(TerrainType type, int x, int y, int z)
    {
        this.type = type;
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
}
