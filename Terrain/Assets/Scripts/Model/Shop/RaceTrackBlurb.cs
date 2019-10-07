using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaceTrackBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        RaceTrack raceTrack = new RaceTrack();
        blurb.text = raceTrack.Blurb;
        metrics.text = "METRICS\n\n" +
        "Cost: " + raceTrack.InitialBuildMoney.ToString() +
        "\n\nInitial Green Points: " + raceTrack.InitialBuildGreen.ToString() +
        "\nInitial Happiness: " + raceTrack.InitialBuildHappiness.ToString() +
        "\n\nMoney per turn: " + raceTrack.GenerateMoney.ToString() +
        "\nGreen Points per turn: " + raceTrack.GenerateGreen.ToString() +
        "\nHappiness per turn: " + raceTrack.GenerateHappiness.ToString();
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
