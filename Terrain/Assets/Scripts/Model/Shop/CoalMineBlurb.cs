using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CoalMineBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    CoalMine coalMine = new CoalMine();
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
        goldCost.text = coalMine.InitialBuildMoney.ToString();
        if(coalMine.InitialBuildGreen > 0){
            greenPointsCost.text = "+"+coalMine.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = coalMine.InitialBuildGreen.ToString();
        }
        if(coalMine.InitialBuildHappiness > 0){
            happinessCost.text = "+"+coalMine.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = coalMine.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = coalMine.Blurb;
        buildingName.text = coalMine.Name;
        coinMetric.text = coalMine.GenerateMoney.ToString() + " / turn";
        greenMetric.text = coalMine.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = coalMine.GenerateHappiness.ToString() + " / turn";
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
