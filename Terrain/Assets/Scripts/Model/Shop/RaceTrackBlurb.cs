using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class RaceTrackBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    RaceTrack raceTrack = new RaceTrack();
    public Text blurb;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI coinMetric;
    public TextMeshProUGUI greenMetric;
    public TextMeshProUGUI happinessMetric;
    public GameObject panel;
    public Button endTurn;

    public Text goldCost;
    public Text greenPointsCost;
    public Text happinessCost;
    
    // Start is called before the first frame update
    void Start()
    {
        goldCost.text = " "+raceTrack.InitialBuildMoney.ToString();
        if(raceTrack.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+raceTrack.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+raceTrack.InitialBuildGreen.ToString();
        }
        if(raceTrack.InitialBuildHappiness > 0){
            happinessCost.text = " +"+raceTrack.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+raceTrack.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = raceTrack.Blurb;
        buildingName.text = raceTrack.Name;
        coinMetric.text = raceTrack.GenerateMoney.ToString() + " / turn";
        greenMetric.text = raceTrack.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = raceTrack.GenerateHappiness.ToString() + " / turn";
        panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        panel.SetActive(false);
    }
    public void OnPointerClick(PointerEventData pointerEventData) {
        panel.SetActive(false);
        endTurn.interactable = false;
    }
}
