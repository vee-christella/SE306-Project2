using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoalMineBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text blurb;
    public Text metrics;
    public GameObject metricsObject;
    public GameObject blurbObject;
    public GameObject panel;
    public Button endTurn;

    // Start is called before the first frame update
    void Start()
    {
        CoalMine coalMine = new CoalMine();
        blurb.text = coalMine.Blurb;
        metrics.text = "METRICS\n\n" +
        "Cost: " + coalMine.InitialBuildMoney.ToString() +
        "\n\nInitial Green Points: " + coalMine.InitialBuildGreen.ToString() +
        "\nInitial Happiness: " + coalMine.InitialBuildHappiness.ToString() +
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
