using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public GameObject Shop;
    public Button EnergyGeneratorTab;
    public GameObject EnergyGeneratorShop;
    public Button UtilitiesTab;
    public GameObject UtilitiesShop;
    public Button RecreationalTab;
    public GameObject RecreationalShop;
    public Button OtherTab;
    public GameObject OtherShop;
   public void OpenShop(){
       Shop.SetActive(true);
       Debug.Log("clicked");
       Time.timeScale = 1;
   }

   public void CloseShop(){
       Shop.SetActive(false);
       Time.timeScale = 1;
   }

   public void OpenEnergyGeneratorTab(){
       reset();
       EnergyGeneratorTab.interactable = false;
       EnergyGeneratorShop.SetActive(true);
   }
   public void OpenUtilitiesTab(){
       reset();
       UtilitiesTab.interactable = false;
       UtilitiesShop.SetActive(true);
   }
   public void OpenRecreationalTab(){
       reset();
       RecreationalTab.interactable = false;
       RecreationalShop.SetActive(true);
   }
   public void OpenOtherTab(){
       reset();
       OtherTab.interactable = false;
       OtherShop.SetActive(true);
   }


   private void reset(){
       EnergyGeneratorTab.interactable = true;
       UtilitiesTab.interactable = true;
       RecreationalTab.interactable = true;
       OtherTab.interactable = true;
       EnergyGeneratorShop.SetActive(false);
       UtilitiesShop.SetActive(false);
       RecreationalShop.SetActive(false);
       OtherShop.SetActive(false);
   }

}
