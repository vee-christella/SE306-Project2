using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class AchievementManager
{
    public Achievement[] achievements;
    public static AchievementManager Instance { get; protected set; }

    public AchievementManager(){
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
        achievements[0].CurrentCount = PlayerPrefs.GetInt("Count"+ achievements[0].Title, 0);
        achievements[1].Title = "Win";
        achievements[1].Blurb = "You have won the game many times";
        achievements[1].HowToComplete = "Win the game 5 times";
        achievements[1].CountToComplete = 5;
        achievements[1].CurrentCount = PlayerPrefs.GetInt("Count"+ achievements[1].Title, 0);
        achievements[2].Title = "Build Nuclear Plant";
        achievements[2].Blurb = "Be careful with your nuclear power plants, thats a lot of nuclear waste you're making.";
        achievements[2].HowToComplete = "Build 5 Nuclear Power plants";
        achievements[2].CountToComplete = 5;
        achievements[2].CurrentCount = PlayerPrefs.GetInt("Count"+ achievements[2].Title, 0);
        achievements[3].Title = "Build Oil Rig";
        achievements[3].Blurb = "Thats a lot of Oil you're digging up. Its not very sustainable.";
        achievements[3].HowToComplete = "Build 5 Oil Rigs";
        achievements[3].CountToComplete = 5;
        achievements[3].CurrentCount = PlayerPrefs.GetInt("Count"+ achievements[3].Title, 0);
        achievements[4].Title = "Win all levels";
        achievements[4].Blurb = "Congratulations, you truly are a climate change master! You will become a great mayor one day :)";
        achievements[4].HowToComplete = "Win all levels";
        achievements[4].CountToComplete = 3;
        achievements[4].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[4].Title, 0);
        achievements[5].Title = "Happiness";
        achievements[5].Blurb = "Wow! You sure know how to keep your city happy!";
        achievements[5].HowToComplete = "Win without letting your city reach negative happiness";
        achievements[5].CountToComplete = 1;
        achievements[5].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[5].Title, 0);
        achievements[6].Title = "We will rebuild!";
        achievements[6].Blurb = "Don't give up! Bad things happen, the important thing is to stay strong!";
        achievements[6].HowToComplete = "Fix a building when it is destroyed by a random event";
        achievements[6].CountToComplete = 1;
        achievements[6].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[6].Title, 0);
        achievements[7].Title = "Win in 20 turns";
        achievements[7].Blurb = "Awesome job! You are a master of preventing climate change!";
        achievements[7].HowToComplete = "Win the game within 20 turns in any level";
        achievements[7].CountToComplete = 20;
        achievements[7].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[7].Title, 0);
        achievements[8].Title = "Win in 30 turns";
        achievements[8].Blurb = "Great job! You are an expert in preventing climate change!";
        achievements[8].HowToComplete = "Win the game within 30 turns in any level";
        achievements[8].CountToComplete = 30;
        achievements[8].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[8].Title, 0);
        achievements[9].Title = "Win in 40 turns";
        achievements[9].Blurb = "Nice job! You have prevented climate change with time to spare!";
        achievements[9].HowToComplete = "Win the game within 40 turns in any level";
        achievements[9].CountToComplete = 40;
        achievements[9].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[9].Title, 0);

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
                    Debug.Log("Achievement callback being called");
                    achievements[(int) type].CallbackAchievementCount(achievements[(int) type]);
                }
            }
        }
    }


}
