using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AchievementManager
{
    Achievement[] achievements;
    public static AchievementManager Instance { get; protected set; }

    public AchievementManager(){
        Instance = this;
        achievements = new Achievement[Enum.GetNames(typeof(AchievementType)).Length];
        foreach(AchievementType achievementType in Enum.GetValues(typeof(AchievementType))){
            achievements[(int)achievementType]= new Achievement(achievementType);
            Debug.Log(achievementType);
        }
        achievements[0].Title = "title";
        achievements[0].Blurb = "title";
        achievements[0].HowToComplete = "title";
        achievements[0].CountToComplete = 5;
    }

    public static AchievementManager GetAchievementManager(){
        if(Instance==null){
            return new AchievementManager();
        }else{
            return Instance;
        }
    }

    public void increaseAchievementCounter(AchievementType type){
        if(!achievements[(int) type].AchievementComplete){
            if(achievements[(int) type].upAchievementCounter()){
                Debug.Log("AchievementManagerComplete: " + achievements[(int) type].Title);
                if(achievements[(int) type].CallbackAchievementCount!= null){
                    achievements[(int) type].CallbackAchievementCount(achievements[(int) type]);
                }
            }
        }
    }
}
