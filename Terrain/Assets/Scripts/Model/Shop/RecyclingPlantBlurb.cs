using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class RecyclingPlantBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    RecyclingPlant recyclingPlant = new RecyclingPlant();
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
        goldCost.text = " "+recyclingPlant.InitialBuildMoney.ToString();
        if(recyclingPlant.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+recyclingPlant.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+recyclingPlant.InitialBuildGreen.ToString();
        }
        if(recyclingPlant.InitialBuildHappiness > 0){
            happinessCost.text = " +"+recyclingPlant.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+recyclingPlant.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = recyclingPlant.Blurb;
        buildingName.text = recyclingPlant.Name;
        coinMetric.text = recyclingPlant.GenerateMoney.ToString() + " per turn";
        greenMetric.text = recyclingPlant.GenerateGreen.ToString() + " per turn";
        happinessMetric.text = recyclingPlant.GenerateHappiness.ToString() + " per turn";
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
