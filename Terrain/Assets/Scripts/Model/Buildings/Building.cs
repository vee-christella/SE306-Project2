using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    float initialBuildMoney;
    float initialBuildGreen;
    float initialBuildHappiness;
    float generateMoney;
    float generateGreen;
    float generateHappiness;
    int id = 0;

    public float InitialBuildMoney { get => initialBuildMoney; set => initialBuildMoney = value; }
    public float InitialBuildGreen { get => initialBuildGreen; set => initialBuildGreen = value; }
    public float InitialBuildHappiness { get => initialBuildHappiness; set => initialBuildHappiness = value; }
    public float GenerateMoney { get => generateMoney; set => generateMoney = value; }
    public float GenerateGreen { get => generateGreen; set => generateGreen = value; }
    public float GenerateHappiness { get => generateHappiness; set => generateHappiness = value; }
    public int Id { get => id; set => id = value; }

    // Constructor to initialise the building with their respective stats
    public Building(float initBuildMoney, float initBuildGreen, 
    float initBuildHappiness, float genMoney, float genGreen)
    {
        this.initialBuildMoney = initBuildMoney;
        this.initialBuildGreen = initBuildGreen;
        this.initialBuildHappiness = initBuildHappiness;

        this.generateGreen = genGreen;
        this.generateMoney = genMoney;
    }

    public Building(float initBuildMoney, float initBuildGreen,
    float initBuildHappiness, float genMoney, float genGreen, float genHappiness)
    {
        this.initialBuildMoney = initBuildMoney;
        this.initialBuildGreen = initBuildGreen;
        this.initialBuildHappiness = initBuildHappiness;

        this.generateGreen = genGreen;
        this.generateMoney = genMoney;
        this.generateHappiness = genHappiness;
    }
}

