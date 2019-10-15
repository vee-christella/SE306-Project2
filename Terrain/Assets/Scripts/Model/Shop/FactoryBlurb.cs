using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class FactoryBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Factory factory = new Factory();
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
        goldCost.text = " "+factory.InitialBuildMoney.ToString();
        if(factory.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+factory.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+factory.InitialBuildGreen.ToString();
        }
        if(factory.InitialBuildHappiness > 0){
            happinessCost.text = " +"+factory.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+factory.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = factory.Blurb;
        buildingName.text = factory.Name;
        coinMetric.text = factory.GenerateMoney.ToString() + " per turn";
        greenMetric.text = factory.GenerateGreen.ToString() + " per turn";
        happinessMetric.text = factory.GenerateHappiness.ToString() + " per turn";
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
