using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ForestBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Forest forest = new Forest();
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
        goldCost.text = forest.InitialBuildMoney.ToString();
        if(forest.InitialBuildGreen > 0){
            greenPointsCost.text = "+"+forest.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = forest.InitialBuildGreen.ToString();
        }
        if(forest.InitialBuildHappiness > 0){
            happinessCost.text = "+"+forest.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = forest.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = forest.Blurb;
        buildingName.text = forest.Name;
        coinMetric.text = forest.GenerateMoney.ToString() + " / turn";
        greenMetric.text = forest.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = forest.GenerateHappiness.ToString() + " / turn";
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
