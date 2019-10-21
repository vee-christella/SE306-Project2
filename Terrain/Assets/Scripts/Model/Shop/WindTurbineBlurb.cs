using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class WindTurbineBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    WindTurbine windTurbine = new WindTurbine();
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
        goldCost.text = windTurbine.InitialBuildMoney.ToString();
        if(windTurbine.InitialBuildGreen > 0){
            greenPointsCost.text = "+"+windTurbine.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = windTurbine.InitialBuildGreen.ToString();
        }
        if(windTurbine.InitialBuildHappiness > 0){
            happinessCost.text = "+"+windTurbine.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = windTurbine.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = windTurbine.Blurb;
        buildingName.text = windTurbine.Name;
        coinMetric.text = windTurbine.GenerateMoney.ToString() + " / turn";
        greenMetric.text = windTurbine.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = windTurbine.GenerateHappiness.ToString() + " / turn";
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
