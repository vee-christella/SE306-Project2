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
        PlayerPrefs.DeleteAll();
        Instance = this;
        achievements = new Achievement[Enum.GetNames(typeof(AchievementType)).Length];
        foreach(AchievementType achievementType in Enum.GetValues(typeof(AchievementType))){
            achievements[(int)achievementType]= new Achievement(achievementType);
            Debug.Log(achievementType);
        }
        achievements[0].Title = "Fail";
        achievements[0].Blurb = "You have lost the Game many times";
        achievements[0].HowToComplete = "Lose the game 5 times";
        achievements[0].CountToComplete = 5;
        achievements[0].CurrentCount= PlayerPrefs.GetInt("Count"+ achievements[0].Title, 0);
        achievements[1].Title = "Win";
        achievements[1].Blurb = "You have won the game many times";
        achievements[1].HowToComplete = "Win the game 5 times";
        achievements[1].CountToComplete = 5;
        achievements[1].CurrentCount= PlayerPrefs.GetInt("Count"+ achievements[1].Title, 0);
        achievements[2].Title = "Build Nuclear Plant";
        achievements[2].Blurb = "Be careful with your nuclear power plants, thats a lot of nuclear waste you're making";
        achievements[2].HowToComplete = "Build 5 Nuclear Power plants";
        achievements[2].CountToComplete = 5;
        achievements[2].CurrentCount= PlayerPrefs.GetInt("Count"+ achievements[2].Title, 0);
        achievements[3].Title = "Build Oil Rig";
        achievements[3].Blurb = "Thats a lot of Oil you're digging up. Its not very sustainable";
        achievements[3].HowToComplete = "Build 5 Oil Rigs";
        achievements[3].CountToComplete = 5;
        achievements[3].CurrentCount= PlayerPrefs.GetInt("Count"+ achievements[3].Title, 0);


        //AchievementManager.GetAchievementManager().increaseAchievementCounter(AchievementType.BuildOlilRig);
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
                Debug.Log("Achievement Complete: " + achievements[(int) type].Title);
                if(achievements[(int) type].CallbackAchievementCount!= null){
                    achievements[(int) type].CallbackAchievementCount(achievements[(int) type]);
                }
            }
        }
    }
}
