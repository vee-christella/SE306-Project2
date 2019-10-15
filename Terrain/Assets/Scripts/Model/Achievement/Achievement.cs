using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Achievement
{
    AchievementType type;
    string title;
    string blurb;
    string howToComplete;
    int countToComplete = 10;
    int currentCount = 0;
    bool achievementComplete = false;
    static Achievement[] achievements;
    Action<Achievement> callbackAchievementCount;

    public Achievement(AchievementType type){
        this.type = type;
    }

    public AchievementType Type{get => type;}
    public string Title{get => title; set => title = value;}
    public string Blurb{get => blurb; set => blurb = value;}
    public string HowToComplete{get => howToComplete; set => howToComplete = value;}
    public int CountToComplete{get => countToComplete; set => countToComplete = value;}
    public int CurrentCount{get => currentCount; 
        set {
            currentCount = value;
            PlayerPrefs.SetInt("Count"+ Title, CurrentCount);
            if(this.CurrentCount>=this.CountToComplete){
                this.AchievementComplete = true;
            }
        }
    }
    public bool AchievementComplete{get => achievementComplete; set => achievementComplete = value;}
    public Achievement[] Achievements{get => achievements; set => achievements = value;}
    public Action<Achievement> CallbackAchievementCount { get => callbackAchievementCount; }

    public static Achievement getThis(AchievementType type){
        return achievements[(int) type];
    }

    public void registerMethodCallbackAchievementCount(Action<Achievement> method)
    {
        callbackAchievementCount += method;
    }

    public bool upAchievementCounter(){
        this.CurrentCount = this.CurrentCount+1;
                Debug.Log("Achievement Counter Increase: "+Title);
        return achievementComplete;
    }

}
