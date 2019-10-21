using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GreenhouseBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Greenhouse greenhouse = new Greenhouse();
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
        goldCost.text = " "+greenhouse.InitialBuildMoney.ToString();
        if(greenhouse.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+greenhouse.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+greenhouse.InitialBuildGreen.ToString();
        }
        if(greenhouse.InitialBuildHappiness > 0){
            happinessCost.text = " +"+greenhouse.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+greenhouse.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = greenhouse.Blurb;
        buildingName.text = greenhouse.Name;
        coinMetric.text = greenhouse.GenerateMoney.ToString() + " / turn";
        greenMetric.text = greenhouse.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = greenhouse.GenerateHappiness.ToString() + " / turn";
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
