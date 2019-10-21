using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRain : Event
{

    private double probability;

    public AcidRain(Game game) :base (-100,-5,-50)
    {
        this.Type = EventType.BuildingDestroyer;
        this.Description = "Acid rain contains lots of acid and is very destructive for buildings and the environment. They come from the bad effects of coal-burning power plants, factories, and cars. When humans burn fossil fuels, toxic fumes are released into the atmosphere, and when it reaches Earth, it makes rain turn into acid rain which enters water systems and sinks into the soil";
        this.Game = game;
        this.DestroysBuildings = true;
        this.Title = "Acid Rain";
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
                    if (random <= 10)
                    {
                        costToRepair = costToRepair + tiles[i,j].Building.InitialBuildMoney;
                        Debug.Log(costToRepair + " cost to repair");
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
/*
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
