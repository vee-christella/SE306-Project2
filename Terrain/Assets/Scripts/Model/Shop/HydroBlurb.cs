using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HydroBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Hydro hydro = new Hydro();
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
        goldCost.text = hydro.InitialBuildMoney.ToString();
        if(hydro.InitialBuildGreen > 0){
            greenPointsCost.text = "+"+hydro.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = hydro.InitialBuildGreen.ToString();
        }
        if(hydro.InitialBuildHappiness > 0){
            happinessCost.text = "+"+hydro.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = hydro.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = hydro.Blurb;
        buildingName.text = hydro.Name;
        coinMetric.text = hydro.GenerateMoney.ToString() + " / turn";
        greenMetric.text = hydro.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = hydro.GenerateHappiness.ToString() + " / turn";
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
