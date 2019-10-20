using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AnimalFarmBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    AnimalFarm animalFarm = new AnimalFarm();
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
        goldCost.text = " "+animalFarm.InitialBuildMoney.ToString();
        if(animalFarm.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+animalFarm.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+animalFarm.InitialBuildGreen.ToString();
        }
        if(animalFarm.InitialBuildHappiness > 0){
            happinessCost.text = " +"+animalFarm.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+animalFarm.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = animalFarm.Blurb;
        buildingName.text = animalFarm.Name;
        coinMetric.text = animalFarm.GenerateMoney.ToString() + " per turn";
        greenMetric.text = animalFarm.GenerateGreen.ToString() + " per turn";
        happinessMetric.text = animalFarm.GenerateHappiness.ToString() + " per turn";
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
