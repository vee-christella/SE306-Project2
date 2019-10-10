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
    
    public Text goldCost;
    public Text greenPointsCost;
    public Text happinessCost;
    public Text name;

    // Start is called before the first frame update
    void Start()
    {
        MovieTheatre movieTheatre = new MovieTheatre();
        goldCost.text = " "+movieTheatre.InitialBuildMoney.ToString();
        greenPointsCost.text = " "+movieTheatre.InitialBuildGreen.ToString();
        happinessCost.text = " "+movieTheatre.InitialBuildHappiness.ToString()+"%";
        blurb.text = movieTheatre.Blurb;
        metrics.text = "METRICS"+
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
