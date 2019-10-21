using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MovieTheatreBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    MovieTheatre movieTheatre = new MovieTheatre();
    public Text blurb;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI coinMetric;
    public TextMeshProUGUI greenMetric;
    public TextMeshProUGUI happinessMetric;
    public GameObject panel;
    public Button endTurn;

    public TextMeshProUGUI goldCost;
    public TextMeshProUGUI greenPointsCost;
    public TextMeshProUGUI happinessCost;
    
    // Start is called before the first frame update
    void Start()
    {
        goldCost.text = movieTheatre.InitialBuildMoney.ToString();
        if(movieTheatre.InitialBuildGreen > 0){
            greenPointsCost.text = "+"+movieTheatre.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = movieTheatre.InitialBuildGreen.ToString();
        }
        if(movieTheatre.InitialBuildHappiness > 0){
            happinessCost.text = "+"+movieTheatre.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = movieTheatre.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = movieTheatre.Blurb;
        buildingName.text = movieTheatre.Name;
        coinMetric.text = movieTheatre.GenerateMoney.ToString() + " / turn";
        greenMetric.text = movieTheatre.GenerateGreen.ToString() + " / turn";
        happinessMetric.text = movieTheatre.GenerateHappiness.ToString() + " / turn";
        panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        panel.SetActive(false);
    }
    public void OnPointerClick(PointerEventData pointerEventData) {
        panel.SetActive(false);
        //endTurn.interactable = false;
    }
}
