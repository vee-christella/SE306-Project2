using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class NuclearBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Nuclear nuclear = new Nuclear();
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
        goldCost.text = " "+nuclear.InitialBuildMoney.ToString();
        if(nuclear.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+nuclear.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+nuclear.InitialBuildGreen.ToString();
        }
        if(nuclear.InitialBuildHappiness > 0){
            happinessCost.text = " +"+nuclear.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+nuclear.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = nuclear.Blurb;
        buildingName.text = nuclear.Name;
        coinMetric.text = nuclear.GenerateMoney.ToString() + " / turn";
        greenMetric.text = nuclear.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = nuclear.GenerateHappiness.ToString() + " / turn";
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
