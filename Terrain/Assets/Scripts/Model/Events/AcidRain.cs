using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRain : Event
{

    private double probability;

    public AcidRain(Game game) :base (-5,-1,-5)
    {
        this.Type = EventType.Random;
        this.Description = "Acid rain contains high levels of nitric and sulfuric acids and comes in the form of snow, fog, and tiny bits of dry material that settles to Earth. The biggest sources are coal-burning power plants, factories, and automobiles. When humans burn fossil fuels, sulfur dioxide (SO2) and nitrogen oxides (NOx) are released into the atmosphere and when it reaches Earth, it flows across the surface in runoff water, enters water systems, and sinks into the soil";
        this.Game = game;
    }

    public double Probability { get => probability; set => probability = value; }

    public override void TileDelta(Tile[,] tiles, bool doDestroyBuildings)
    {

        if (!doDestroyBuildings)
        {
            return;
        }

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].Building != null)
                {
                    Debug.Log("Found a tile with a building");
                    int random = Random.Range(0, 101);
                    // 5% chance to destory building on tile
                    if (random < 5)
                    {
                        float buildingGreenGen = tiles[i, j].Building.GenerateGreen;
                        float buildingMoneyGen = tiles[i, j].Building.GenerateMoney;
                        float buildingHappinessGen = tiles[i, j].Building.GenerateHappiness;

                        if (tiles[i,j].removeBuilding())
                        {
                            Game.GenerateGreen = Game.GenerateGreen - buildingGreenGen;
                            Game.GenerateMoney = Game.GenerateMoney - buildingMoneyGen;
                            Game.GenerateHappiness = Game.GenerateHappiness - buildingHappinessGen;
                        }
                    }
                }
            }
        }

    }

}
