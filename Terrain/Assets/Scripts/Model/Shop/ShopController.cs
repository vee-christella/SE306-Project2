using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    public GameObject Shop;
   public void OpenShop(){
       Shop.SetActive(true);
       Debug.Log("clicked");
       Time.timeScale = 1;
   }

   public void CloseShop(){
       Shop.SetActive(false);
       Time.timeScale = 1;
   }
}
