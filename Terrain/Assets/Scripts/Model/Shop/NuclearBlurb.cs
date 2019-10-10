using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NuclearBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        Nuclear nuclear = new Nuclear();
        goldCost.text = " "+nuclear.InitialBuildMoney.ToString();
        greenPointsCost.text = " "+nuclear.InitialBuildGreen.ToString();
        happinessCost.text = " "+nuclear.InitialBuildHappiness.ToString()+"%";
        blurb.text = nuclear.Blurb;
        metrics.text = "METRICS"+
        "\n\nMoney per turn: " + nuclear.GenerateMoney.ToString() +
        "\nGreen Points per turn: " + nuclear.GenerateGreen.ToString() +
        "\nHappiness per turn: " + nuclear.GenerateHappiness.ToString();
        metricsObject.SetActive(false);
        panel.SetActive(false);
        blurbObject.SetActive(false);
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
