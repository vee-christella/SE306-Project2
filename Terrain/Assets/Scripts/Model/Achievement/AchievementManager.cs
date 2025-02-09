﻿using System.Collections;
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
        achievements[0].Title = "Winner";
        achievements[0].Blurb = "Congratulations on winning the game.";
        achievements[0].HowToComplete = "Win the game";
        achievements[0].CountToComplete = 1;
        achievements[0].CurrentCount = PlayerPrefs.GetInt("Count"+ achievements[0].Title, 0);
        achievements[1].Title = "Climate Change Combater";
        achievements[1].Blurb = "Congratulations on sucessfully combatting climate change!";
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
        achievements[4].Title = "Build Hydro Plant";
        achievements[4].Blurb = "Hydro plants are an eco friendly and sustainable source of energy.";
        achievements[4].HowToComplete = "Build 10 hydro plants";
        achievements[4].CountToComplete = 10;
        achievements[4].CurrentCount = PlayerPrefs.GetInt("Count"+ achievements[4].Title, 0);
        achievements[5].Title = "Win all levels";
        achievements[5].Blurb = "Congratulations, you truly are a climate change master! You will become a great mayor one day :)";
        achievements[5].HowToComplete = "Win all levels";
        achievements[5].CountToComplete = 3;
        achievements[5].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[5].Title, 0);
        achievements[6].Title = "Happiness";
        achievements[6].Blurb = "Wow! You sure know how to keep your city happy!";
        achievements[6].HowToComplete = "Reach 100% happiness";
        achievements[6].CountToComplete = 1;
        achievements[6].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[6].Title, 0);
        achievements[7].Title = "Win in 80 turns";
        achievements[7].Blurb = "Awesome job! You are a master of preventing climate change!";
        achievements[7].HowToComplete = "Win the game within 80 turns in any level";
        achievements[7].CountToComplete = 1;
        achievements[7].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[7].Title, 0);
        achievements[8].Title = "Win in 90 turns";
        achievements[8].Blurb = "Great job! You are an expert in preventing climate change!";
        achievements[8].HowToComplete = "Win the game within 90 turns in any level";
        achievements[8].CountToComplete = 1;
        achievements[8].CurrentCount = PlayerPrefs.GetInt("Count" + achievements[8].Title, 0);
        achievements[9].Title = "Lose5";
        achievements[9].Blurb = "You have lost the Game many times. Don't let climate change win!";
        achievements[9].HowToComplete = "Lose the game 5 times";
        achievements[9].CountToComplete = 5;
        achievements[9].CurrentCount = PlayerPrefs.GetInt("Count"+ achievements[9].Title, 0);
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
                if(achievements[(int) type].CallbackAchievementCount!= null){
                    achievements[(int) type].CallbackAchievementCount(achievements[(int) type]);
                }
            }
        }
    }


}
