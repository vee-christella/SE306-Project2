﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BeeFarmBlurb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    BeeFarm beeFarm = new BeeFarm();
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
        goldCost.text = " "+beeFarm.InitialBuildMoney.ToString();
        if(beeFarm.InitialBuildGreen > 0){
            greenPointsCost.text = " +"+beeFarm.InitialBuildGreen.ToString();
        } else {
            greenPointsCost.text = " "+beeFarm.InitialBuildGreen.ToString();
        }
        if(beeFarm.InitialBuildHappiness > 0){
            happinessCost.text = " +"+beeFarm.InitialBuildHappiness.ToString()+"%";
        } else {
            happinessCost.text = " "+beeFarm.InitialBuildHappiness.ToString()+"%";
        }
        panel.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData){
        blurb.text = beeFarm.Blurb;
        buildingName.text = beeFarm.Name;
        coinMetric.text = beeFarm.GenerateMoney.ToString() + " per turn";
        greenMetric.text = beeFarm.GenerateGreen.ToString() + " per turn";
        happinessMetric.text = beeFarm.GenerateHappiness.ToString() + " per turn";
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