using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AchievementManager : MonoBehaviour
{
    Achievement[] achievements;
    public static AchievementManager Instance { get; protected set; }

    public AchievementManager(){
        Instance = this;
        achievements = new Achievement[Enum.GetNames(typeof(AchievementType)).Length];
        foreach(AchievementType achievementType in Enum.GetValues(typeof(AchievementType))){
            achievements[(int)achievementType]= new Achievement(achievementType);
        }
        achievements[0].Title = "title";
        achievements[0].Blurb = "title";
        achievements[0].HowToComplete = "title";
        achievements[0].CountToComplete = 5;
    }
}
