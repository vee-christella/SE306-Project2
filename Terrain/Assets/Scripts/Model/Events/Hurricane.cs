using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : Event
{
    private double probability;


    public Hurricane(Game game) :base (-5,-1,-5)
    {
        this.Type = EventType.BuildingDestroyer;
        this.Game = game;
        this.DestroysBuildings = true;
        this.Description = "The intensity, frequency and duration of the strongest hurricanes have all increased since the early 1980s. Hurricane-associated storms and rainfall rates will increase as the climate becomes warmer.";
    }

    public double Probability { get => probability; set => probability = value; }

    /*public override float CalculateCostToRepair(Tile[,] tiles)
    {
        float costToRepair = 0;

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Building != null)
                {
                    if (tiles[i, j].Building.GetType().Name.ToString() == "TownHall")
                    {
                        continue;
                    }
                    Debug.Log("Found a tile with a building");
                    int random = Random.Range(0, 100);
                    // 10% chance to destory building on tile
                    if (random <= 100)
                    {
                        costToRepair = costToRepair + (tiles[i, j].Building.InitialBuildMoney / 5);
                    }
                }
            }
        }

        return costToRepair;
    }*/

    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {
        if (doDestroyBuildings)
        {
            DestroyBuildings();
        }

        /*for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Building != null)
                {
                    if (tiles[i, j].Building.GetType().Name.ToString() == "TownHall")
                    {
                        continue;
                    }

                    Debug.Log("Found a tile with a building");
                    int random = Random.Range(0, 100);
                    // 10% chance to destory building on tile
                    if (random <= 10)
                    {
                        float buildingGreenGen = tiles[i, j].Building.GenerateGreen;
                        float buildingMoneyGen = tiles[i, j].Building.GenerateMoney;
                        float buildingHappinessGen = tiles[i, j].Building.GenerateHappiness;

                        if (tiles[i, j].removeBuilding())
                        {
                            Game.GenerateGreen = Game.GenerateGreen - buildingGreenGen;
                            Game.GenerateMoney = Game.GenerateMoney - buildingMoneyGen;
                            Game.GenerateHappiness = Game.GenerateHappiness - buildingHappinessGen;
                        }
                    }
                }
            }
        }*/
    }
}
