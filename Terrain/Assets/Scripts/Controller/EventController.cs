using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventController : MonoBehaviour
{

    public GameObject eventPopupPanel;

    public TextMeshProUGUI eventInfo;
    public TextMeshProUGUI moneyEffect;
    public TextMeshProUGUI greenPointsEffect;
    public TextMeshProUGUI happinessEffect;

    private bool doDestroyBuildings;

    public bool DoDestroyBuildings { get => doDestroyBuildings; set => doDestroyBuildings = value; }

    public EventController()
    {
        this.doDestroyBuildings = false;
    }


    public void DisplayPopup(Event gameEvent)
    {
        doDestroyBuildings = true;

        if (gameEvent.Type == Event.EventType.BuildingDestoryer)
        {
            //display extra info and give user option to pay gold to avoid building destruction
        }

        eventInfo.text = "A  " + gameEvent.GetType().Name.ToString() + " has occurred! \n";

        moneyEffect.text = DisplayEffect(gameEvent.MoneyDelta) + gameEvent.MoneyDelta;
        greenPointsEffect.text = DisplayEffect(gameEvent.GreenPointDelta) + gameEvent.GreenPointDelta;
        happinessEffect.text = DisplayEffect(gameEvent.HappinessDelta) + gameEvent.HappinessDelta;

        eventPopupPanel.SetActive(true);
    }

    public void RemovePopup()
    {
        eventPopupPanel.SetActive(false);
    }

    // Configure which sign to display in the random event section
    private string DisplayEffect(int effect)
    {
        if (effect < 0)
        {
            return "";
        }
        else
        {
            return "+ ";
        }
    }
    
}


