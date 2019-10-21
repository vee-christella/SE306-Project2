using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class VegetableFarmBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    VegetableFarm vegetableFarm = new VegetableFarm();
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
        goldCost.text = vegetableFarm.InitialBuildMoney.ToString();
        if(vegetableFarm.InitialBuildGreen > 0){
            greenPointsCost.text = "+"+vegetableFarm.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = vegetableFarm.InitialBuildGreen.ToString();
        }
        if(vegetableFarm.InitialBuildHappiness > 0){
            happinessCost.text = "+"+vegetableFarm.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = vegetableFarm.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = vegetableFarm.Blurb;
        buildingName.text = vegetableFarm.Name;
        coinMetric.text = vegetableFarm.GenerateMoney.ToString() + " / turn";
        greenMetric.text = vegetableFarm.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = vegetableFarm.GenerateHappiness.ToString() + " / turn";
        panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        panel.SetActive(false);
    }
    public void OnPointerClick(PointerEventData pointerEventData) {
        panel.SetActive(false);
        //endTurn.interactable = false;
    }
}
