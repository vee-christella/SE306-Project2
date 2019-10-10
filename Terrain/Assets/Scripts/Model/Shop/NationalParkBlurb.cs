using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NationalParkBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        NationalPark nationalPark = new NationalPark();
        blurb.text = nationalPark.Blurb;
        metrics.text = "METRICS\n\n" +
        "Cost: " + nationalPark.InitialBuildMoney.ToString() +
        "\n\nInitial Green Points: " + nationalPark.InitialBuildGreen.ToString() +
        "\nInitial Happiness: " + nationalPark.InitialBuildHappiness.ToString() +
        "\n\nMoney per turn: " + nationalPark.GenerateMoney.ToString() +
        "\nGreen Points per turn: " + nationalPark.GenerateGreen.ToString() +
        "\nHappiness per turn: " + nationalPark.GenerateHappiness.ToString();
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
