using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    int rows;
    int columns;
    int turn;
    Tile[,] tiles;
    float money;
    float green;
    float happiness;
    float generateMoney;
    float generateGreen;
    float generateHappiness;
    Building[,] buildings;

    public int Rows { get => rows;}
    public int Columns { get => columns;}
    public float Money { get => money; set => money = value; }
    public float Green { get => green; set => green = value; }
    public float Happiness { get => happiness; set => happiness = value; }
    public float GenerateMoney { get => generateMoney; set => generateMoney = value; }
    public float GenerateGreen { get => generateGreen; set => generateGreen = value; }
    public float GenerateHappiness { get => generateHappiness; set => generateHappiness = value; }
    public int Turn { get => turn; set => turn = value; }

    public Game(int rows = 30, int columns = 30)
    {
        this.rows = rows;
        this.columns = columns;
        tiles = new Tile[rows, columns];
        buildings = new Building[rows, columns];
        for(int i=0; i<rows; i++)
        {
            for(int j =0; j<columns; j++)
            {
                tiles[i, j] = new Tile(this, i, j);
            }
        }
        Debug.Log("game created");
    }


    public Tile getTileAt(int x, int y)
    {
        if (x >= rows || x < 0 || y >= columns || y < 0)
        {
            return null;
        }
        return tiles[x, y];
    }

    public void nextTurn()
    {
        Event event = eventForNextTurn();
    
       if (event != null) {
            GenerateGreen = GenerateGreen + event.GreenPointDelta;
            GenerateMoney = GenerateMoney + event.MoneyDelta;
            GenerateHappiness = GenerateHappiness + event.HappinessDelta;
        }

        Money = Money + GenerateMoney;
        Green = Green + GenerateGreen;
        Happiness = Happiness + GenerateHappiness;
    }

public Event eventForNextTurn()
{

    List<Event> randomEventList = initaliseRandomEventList();
    Event event;

    if (Turn == 10)
    {
        event = new Drought();
    }
    
    else if (random.Next(1, 100) < 10)
    {
        event = randomEventList[random.Next(0,randomEventList.Count)];
    }

    return event;
}

private List<Event> initaliseRandomEventList() 
{
    List<Event> randomEventList = new List<Event>();

    randomEventList.Add(new AcidRain());
    randomEventList.Add(new Earthquake());
    randomEventList.Add(new ForestSpawn());
    randomEventList.Add(new Tsunami());   
}
