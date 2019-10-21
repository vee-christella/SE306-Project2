using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    public GameObject achievementPopup;
    public GameObject achievementPrefab;
    private System.Action popup;
    public GameObject achievementsPanel;

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
            if (achievement.CurrentCount == achievement.CountToComplete)
            {
                achievement.AchievementRef.transform.GetChild(3).GetComponent<Image>().enabled = true;
            }
        }
        achievementsPanel.SetActive(false);
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
    public void ActivatePopUp(Achievement achievement)
    {
        achievement.AchievementRef.transform.GetChild(3).GetComponent<Image>().enabled = true;

        GameObject popup = (GameObject)Instantiate(achievementPopup);
        popup.transform.SetParent(GameObject.Find("AchievementPopups").transform);
        popup.transform.localScale = new Vector3(1, 1, 1);
        popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Achievement complete: " + achievement.Title;
        popup.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = achievement.Blurb;
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        foreach(Achievement achievement in AchievementManager.Instance.achievements){
            achievement.AchievementRef.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerPrefs.GetInt("Count"+ achievement.Title, 0).ToString() + " out of " + achievement.CountToComplete.ToString();
            achievement.AchievementRef.transform.GetChild(3).GetComponent<Image>().enabled = false;
        }
    }
}