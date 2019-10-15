using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ZooBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Zoo zoo = new Zoo();
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
        goldCost.text = " "+zoo.InitialBuildMoney.ToString();
        if(zoo.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+zoo.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+zoo.InitialBuildGreen.ToString();
        }
        if(zoo.InitialBuildHappiness > 0){
            happinessCost.text = " +"+zoo.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+zoo.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = zoo.Blurb;
        buildingName.text = zoo.Name;
        coinMetric.text = zoo.GenerateMoney.ToString() + " per turn";
        greenMetric.text = zoo.GenerateGreen.ToString() + " per turn";
        happinessMetric.text = zoo.GenerateHappiness.ToString() + " per turn";
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
