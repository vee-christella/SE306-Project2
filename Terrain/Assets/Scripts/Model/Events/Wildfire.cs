﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildfire : Event
{
    private double probability;

    public Wildfire(Game game) : base(-100, -3, 0)
    {
        this.Type = EventType.BuildingDestroyer;
        this.Description = "Wildfires occur when fires are naturally started, burning large areas of the environment." +
            " Severe heat and drought make forests drier which fuel wildfires. " + "Events like wildfires have a higher chance of happening the lower your green points are " +
             "and the longer it takes you to reach 1000 green points!";
        this.Game = game;
        this.Title = "Wildfire";
        this.DestroysBuildings = true;
        this.TileDeltaDesc = "Some of your forests have been destroyed.";
    }

    public double Probability { get => probability; set => probability = value; }


    //Overwritten method to say only forrests are destroyed
    public override bool typeToDestroy(Tile tile){
        if(tile.Building.GetType().Name.ToString() == "Forest"){
            return true;
        }
        return false;
    }

//Overwritten method to increase chance of destroying Forrests
    public override bool chanceToDestroy(){
        Debug.Log("wildFire Destroy Chance");
        int random = Random.Range(0, 100);
        if (random <= 30){
            return true;
        }else{
            return false;
        }
    }

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
                if (tiles[i, j].Building.GetType().Name.ToString() == "Forest")
                {
                    Debug.Log("Found a tile with a forest");
                    int random = Random.Range(0, 2);
                    // 50% chance to destroy forest
                    if (random == 1)
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
