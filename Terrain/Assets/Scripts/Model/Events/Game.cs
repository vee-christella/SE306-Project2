using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    int rows;
    int columns;
    Tile[,] tiles;
    float money;
    float green;
    float happiness;
    float generateMoney;
    float generateGreen;
    float generateHappiness;
    Building[,] buildings;
    float currentTurn;
    float maxTurns;
    float maxGreen;

    public int Rows { get => rows;}
    public int Columns { get => columns;}
    public float Money { get => money; set => money = value; }
    public float Green { get => green; set => green = value; }
    public float Happiness { get => happiness; set => happiness = value; }
    public float GenerateMoney { get => generateMoney; set => generateMoney = value; }
    public float GenerateGreen { get => generateGreen; set => generateGreen = value; }
    public float GenerateHappiness { get => generateHappiness; set => generateHappiness = value; }
    public float CurrentTurn { get => currentTurn; set => currentTurn = value; }
    public float MaxTurns { get => maxTurns; set => maxTurns = value; }
    public float MaxGreen { get => maxGreen; set => maxGreen = value; }

    public Game(int rows = 30, int columns = 30)
    {
        this.Money = 100;
        this.Green = 0;
        this.Happiness = 50;
        this.currentTurn = 0;
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

        this.nextTurn();


    }


    public Tile getTileAt(int x, int y)
    {
        if (x >= rows || x < 0 || y >= columns || y < 0)
        {
            return null;
        }
        return tiles[x, y];
    }

    /* This method proceeds with the next turn after the user clicks the 
     * end turn button. It increments the accumulated points and shows it on 
     * the metrics
     */   
    public void nextTurn()
    {
        this.currentTurn++;

        // Increase the metrics
        this.money = Money + GenerateMoney;
        this.green = Green + GenerateGreen;
        this.happiness = Happiness + GenerateHappiness;

        // Check if the user has won the game by reaching the number of green
        // points required
        if (this.green >= maxGreen)
        {
            this.endGame(true);

            // Check if the user has lost the game by exceeding the max number
            // of turns allowed 
        } else if (currentTurn >= maxTurns)
        {
            this.endGame(false);
        }
        else
        {
            // TODO: Method for user actions
        }
    }

    public void endGame(bool isVictory)
    {
        // TODO: Victory/Fail screen goes here
    }


}
