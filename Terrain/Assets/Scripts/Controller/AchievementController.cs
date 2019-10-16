using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementController : MonoBehaviour
{
    public GameObject achievementPopup;
    public TextMeshProUGUI achievementTitle;
    public TextMeshProUGUI achievementDesc;
    

    private System.Action popup;

    private void Start()
    {

        new AchievementManager();
        Achievement[] listOfAchievements = AchievementManager.Instance.achievements;
        foreach(Achievement achievement in AchievementManager.Instance.achievements) {
            achievement.registerMethodCallbackAchievementCount(ActivatePopUp);
        }
    }

    void ActivatePopUp(Achievement achievement)
    {
        Debug.Log("Achievement!!!");
        achievementTitle.text = "Achievement complete: " + achievement.Title;
        achievementDesc.text = achievement.Blurb;
        achievementPopup.SetActive(true);
    }

    public void ClosePopUp()
    {
        achievementPopup.SetActive(false);
    }
}
