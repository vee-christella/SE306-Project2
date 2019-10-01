﻿using System.Collections;
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

    public float InitialBuildMoney { get => initialBuildMoney; set => initialBuildMoney = value; }
    public float InitialBuildGreen { get => initialBuildGreen; set => initialBuildGreen = value; }
    public float InitialBuildHappiness { get => initialBuildHappiness; set => initialBuildHappiness = value; }
    public float GenerateMoney { get => generateMoney; set => generateMoney = value; }
    public float GenerateGreen { get => generateGreen; set => generateGreen = value; }
    public float GenerateHappiness { get => generateHappiness; set => generateHappiness = value; }
}

