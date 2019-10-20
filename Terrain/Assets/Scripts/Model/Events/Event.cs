using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event
{

    public enum EventType
    {
        Good, BuildingDestroyer, TileChanger
    };

    private int greenPointDelta;
    private int happinessDelta;
    private int moneyDelta;
    private EventType type;
    private string description;
    private Game game;
    private List<Tile> destroyingBuildings = new List<Tile>();
    private bool destroysBuildings = false;

    public Event(int greenPointDelta, int happinessDelta, int moneyDelta)
    {
        GreenPointDelta = greenPointDelta;
        HappinessDelta = happinessDelta;
        MoneyDelta = moneyDelta;
    }

    public int GreenPointDelta { get => greenPointDelta; set => greenPointDelta = value; }
    public int HappinessDelta { get => happinessDelta; set => happinessDelta = value; }
    public int MoneyDelta { get => moneyDelta; set => moneyDelta = value; }
    public string Description { get => description; set => description = value; }
    public EventType Type { get => type; set => type = value; }
    public Game Game { get => game; set => game = value; }
    public List<Tile> DestroyingBuildings{get => destroyingBuildings;}
    public bool DestroysBuildings { get => destroysBuildings; set => destroysBuildings = value; }
    public abstract void TileDelta(Tile[,] tiles, bool doDestoryBuildings);

    public float CalculateCostToRepair(Tile[,] tiles){
        float costToRepair = 0;
        if(DestroysBuildings){
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].Building != null)
                    {
                        if (typeToDestroy(tiles[i, j]))
                        {
                            // deafult 10% chance to destory building on tile
                            if (chanceToDestroy())
                            {
                                costToRepair = Mathf.FloorToInt(costToRepair + ((float)(tiles[i,j].Building.InitialBuildMoney * 0.5)));
                                DestroyingBuildings.Add(tiles[i,j]);
                            }
                        }

                    }
                }
            }
        }
        
        return costToRepair;
    }


    //Overridable method for each event to have custom buildings they affect
    public virtual bool typeToDestroy(Tile tile){
        if(tile.Building.GetType().Name.ToString() == "TownHall"){
            return false;
        }
        return true;
    }

    //Overrideable method so Events can have custom chance to destroy building
    public virtual bool chanceToDestroy(){
        Debug.Log("Default Destroy Chance");
        int random = Random.Range(0, 100);
        if (random <= 10){
            return true;
        }else{
            return false;
        }
    }

    public void DestroyBuildings(){
        foreach (Tile tile in this.DestroyingBuildings){
            float cost = tile.Building.InitialBuildMoney * 0.25f;
            tile.removeBuilding();
           // Game.Money+=cost;
        }
        DestroyingBuildings.Clear();
    }
}
