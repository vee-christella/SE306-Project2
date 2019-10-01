using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    float initialMoney;

    public float InitialMoney
    {
        get
        {
            return initialMoney;
        }
        set
        {
            initialMoney = value;
        }
    }

    float initialGreen;

    public float InitialGreen
    {
        get
        {
            return initialGreen;
        }
        set
        {
            initialGreen = value;
        }
    }

    float initialHappiness;

    public float InitialHappiness
    {
        get
        {
            return initialHappiness;
        }
        set
        {
            initialHappiness = value;
        }
    }

    float generateMoney;

    public float GenerateMoney
    {
        get
        {
            return generateMoney;
        }
        set
        {
            generateMoney = value;
        }
    }

    float generateGreen;

    public float GenerateGreen
    {
        get
        {
            return generateGreen;
        }
        set
        {
            generateGreen = value;
        }
    }

    float generateHappiness;

    public float GenerateHappiness
    {
        get
        {
            return generateHappiness;
        }
        set
        {
            generateHappiness = value;
        }
    }



}
