using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OilRefineryBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text blurb;
    public Text metrics;
    public GameObject metricsObject;
    public GameObject blurbObject;
    public GameObject panel;
    public Button endTurn;
    
    public Text goldCost;
    public Text greenPointsCost;
    public Text happinessCost;
    public Text name;

    // Start is called before the first frame update
    void Start()
    {
        OilRefinery oilRefinery = new OilRefinery();
        goldCost.text = " "+oilRefinery.InitialBuildMoney.ToString();
        greenPointsCost.text = " "+oilRefinery.InitialBuildGreen.ToString();
        happinessCost.text = " "+oilRefinery.InitialBuildHappiness.ToString()+"%";
        blurb.text = oilRefinery.Blurb;
        metrics.text = "METRICS"+
        "\n\nMoney per turn: " + oilRefinery.GenerateMoney.ToString() +
        "\nGreen Points per turn: " + oilRefinery.GenerateGreen.ToString() +
        "\nHappiness per turn: " + oilRefinery.GenerateHappiness.ToString();
        panel.SetActive(false);
        blurbObject.SetActive(false);
        metricsObject.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        panel.SetActive(true);
        blurbObject.SetActive(true);
        metricsObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        panel.SetActive(false);
        blurbObject.SetActive(false);
        metricsObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData pointerEventData) {
        panel.SetActive(false);
        blurbObject.SetActive(false);
        metricsObject.SetActive(false);
        endTurn.interactable = false;
    }
}
