using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    public GameObject eventPopupCanvas;


    public void DisplayPopup(Event gameEvent)
    {
        Debug.Log("pop up");
        eventPopupCanvas.SetActive(true);
    }

    public void RemovePopup()
    {
        eventPopupCanvas.SetActive(false);
    }
}
