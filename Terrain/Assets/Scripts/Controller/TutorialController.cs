﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject[] tutorialStages;
    public int tutorialIndex;
    public TutorialController instance;
    
    public GameObject shop;

    public static TutorialController Instance { get; protected set; }

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {

        //Debug.Log("Tutorial Index: " + tutorialIndex);
            //Debug.Log("Index is " + tutorialIndex + " and i is " + i);
            tutorialStages[tutorialIndex].SetActive(true);

            if (tutorialIndex > 0)
            {
                tutorialStages[tutorialIndex - 1].SetActive(false);
            }
  

         if (tutorialIndex == 3)
        {

            Debug.Log(shop.activeSelf);
            if (shop.activeSelf)
            {
                Debug.Log("Tutorial 4 - complete!!");
            }
        }
    }
}
