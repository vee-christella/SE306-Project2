using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SolarFarmBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    SolarFarm solarFarm = new SolarFarm();
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
        goldCost.text = " "+solarFarm.InitialBuildMoney.ToString();
        if(solarFarm.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+solarFarm.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+solarFarm.InitialBuildGreen.ToString();
        }
        if(solarFarm.InitialBuildHappiness > 0){
            happinessCost.text = " +"+solarFarm.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+solarFarm.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = solarFarm.Blurb;
        buildingName.text = solarFarm.Name;
        coinMetric.text = solarFarm.GenerateMoney.ToString() + " per turn";
        greenMetric.text = solarFarm.GenerateGreen.ToString() + " per turn";
        happinessMetric.text = solarFarm.GenerateHappiness.ToString() + " per turn";
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
