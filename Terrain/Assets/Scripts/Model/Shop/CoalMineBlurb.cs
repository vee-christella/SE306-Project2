using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoalMineBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    CoalMine coalMine = new CoalMine();
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
        goldCost.text = " "+coalMine.InitialBuildMoney.ToString();
        greenPointsCost.text = " "+coalMine.InitialBuildGreen.ToString();
        happinessCost.text = " "+coalMine.InitialBuildHappiness.ToString()+"%";
        blurb.text = coalMine.Blurb;
        metrics.text = "METRICS" + 
        "\n\nMoney per turn: " + coalMine.GenerateMoney.ToString() +
        "\nGreen Points per turn: " + coalMine.GenerateGreen.ToString() +
        "\nHappiness per turn: " + coalMine.GenerateHappiness.ToString();
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
