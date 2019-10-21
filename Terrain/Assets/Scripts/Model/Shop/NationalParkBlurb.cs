using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class NationalParkBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    NationalPark nationalPark = new NationalPark();
    public Text blurb;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI coinMetric;
    public TextMeshProUGUI greenMetric;
    public TextMeshProUGUI happinessMetric;
    public GameObject panel;
    public Button endTurn;

    public TextMeshProUGUI goldCost;
    public TextMeshProUGUI greenPointsCost;
    public TextMeshProUGUI happinessCost;
    
    // Start is called before the first frame update
    void Start()
    {
        goldCost.text = nationalPark.InitialBuildMoney.ToString();
        if(nationalPark.InitialBuildGreen > 0){
            greenPointsCost.text = "+"+nationalPark.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = nationalPark.InitialBuildGreen.ToString();
        }
        if(nationalPark.InitialBuildHappiness > 0){
            happinessCost.text = "+"+nationalPark.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = nationalPark.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = nationalPark.Blurb;
        buildingName.text = nationalPark.Name;
        coinMetric.text = nationalPark.GenerateMoney.ToString() + " / turn";
        greenMetric.text = nationalPark.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = nationalPark.GenerateHappiness.ToString() + " / turn";
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
