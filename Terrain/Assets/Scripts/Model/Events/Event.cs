using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event
{

    public enum EventType
    {
        Good, BuildingDestoryer, TileChanger
    };

    private int greenPointDelta;
    private int happinessDelta;
    private int moneyDelta;
    private EventType type;
    private string description;
    private Game game;

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

    public abstract void TileDelta(Tile[,] tiles, bool doDestoryBuildings);

}
