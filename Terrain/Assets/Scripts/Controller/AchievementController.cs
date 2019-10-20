using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementController : MonoBehaviour
{
    public GameObject achievementPopup;
    public TextMeshProUGUI achievementTitle;
    public TextMeshProUGUI achievementDesc;
    public GameObject achievementPrefab;
    private System.Action popup;

    private void Start()
    {
        // PlayerPrefs.DeleteAll();
        new AchievementManager();
        Achievement[] listOfAchievements = AchievementManager.Instance.achievements;
        foreach(Achievement achievement in AchievementManager.Instance.achievements) {
            achievement.registerMethodCallbackAchievementCount(ActivatePopUp);
            achievement.AchievementRef = (GameObject)Instantiate(achievementPrefab);
            CreateAchievement(achievement.AchievementRef, achievement.Title, achievement.HowToComplete, achievement.CurrentCount + " out of " +
            achievement.CountToComplete);
        }

    }
    public void CreateAchievement(GameObject achievement, string title, string description, string progress)
    {
        SetAchievementInfo(achievement, title, description, progress);
    }
    public void SetAchievementInfo(GameObject achievement, string title, string description, string progress)
    {
        achievement.transform.SetParent(GameObject.Find("AchievementsContent").transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
        achievement.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = progress;
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
