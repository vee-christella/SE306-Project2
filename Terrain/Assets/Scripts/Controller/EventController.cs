using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventController : MonoBehaviour
{

    public GameObject eventPopupPanel;

    public TextMeshProUGUI eventInfo;

    public void DisplayPopup(Event gameEvent)
    {
        eventInfo.text = "Event occurance:  " + gameEvent.GetType().Name.ToString() + "\n" +
           "Effects are: Minus " + gameEvent.GreenPointDelta + " green points, " + gameEvent.MoneyDelta + " money points " +
           " and " + gameEvent.HappinessDelta + " happiness points";
        Debug.Log("pop up");
        eventPopupPanel.SetActive(true);
    }

    public void RemovePopup()
    {
        eventPopupPanel.SetActive(false);
    }
}

