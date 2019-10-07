using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovieTheatreBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
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
        MovieTheatre movieTheatre = new MovieTheatre();
        blurb.text = movieTheatre.Blurb;
        metrics.text = "Cost: " + movieTheatre.InitialBuildMoney.ToString() +
        "\n\nInitial Green Points: " + movieTheatre.InitialBuildGreen.ToString() +
        "\nInitial Happiness: " + movieTheatre.InitialBuildHappiness.ToString() +
        "\n\nMoney per turn: " + movieTheatre.GenerateMoney.ToString() +
        "\nGreen Points per turn: " + movieTheatre.GenerateGreen.ToString() +
        "\nHappiness per turn: " + movieTheatre.GenerateHappiness.ToString();
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
