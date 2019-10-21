using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OilRefineryBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    OilRefinery oilRefinery = new OilRefinery();
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
        goldCost.text = oilRefinery.InitialBuildMoney.ToString();
        if(oilRefinery.InitialBuildGreen > 0){
            greenPointsCost.text = "+"+oilRefinery.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = oilRefinery.InitialBuildGreen.ToString();
        }
        if(oilRefinery.InitialBuildHappiness > 0){
            happinessCost.text = "+"+oilRefinery.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = oilRefinery.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = oilRefinery.Blurb;
        buildingName.text = oilRefinery.Name;
        coinMetric.text = oilRefinery.GenerateMoney.ToString() + " / turn";
        greenMetric.text = oilRefinery.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = oilRefinery.GenerateHappiness.ToString() + " / turn";
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
