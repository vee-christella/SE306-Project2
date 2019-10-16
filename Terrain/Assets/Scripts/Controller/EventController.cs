using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventController : MonoBehaviour
{

    public GameObject eventPopupPanel;
    public GameObject buildingDestroyerPopupPanel;
    private Game game;

    public TextMeshProUGUI eventInfo;
    public TextMeshProUGUI moneyEffect;
    public TextMeshProUGUI greenPointsEffect;
    public TextMeshProUGUI happinessEffect;

    public Game Game { get => game; set => game = value; }

    public EventController() {
    }

    public void DisplayPopup()
    {
        Event gameEvent = Game.GameEvent;

        eventInfo.text = "A  " + gameEvent.GetType().Name.ToString() + " has occurred! \n";

        moneyEffect.text = DisplayEffect(gameEvent.MoneyDelta) + gameEvent.MoneyDelta;
        greenPointsEffect.text = DisplayEffect(gameEvent.GreenPointDelta) + gameEvent.GreenPointDelta;
        happinessEffect.text = DisplayEffect(gameEvent.HappinessDelta) + gameEvent.HappinessDelta;

        eventPopupPanel.SetActive(true);
    }

    public void DisplayBuildingDestroyerPopup()
    {
        Event gameEvent = Game.GameEvent;

        eventInfo.text = "A  " + gameEvent.GetType().Name.ToString() + " has occurred! \n";

        moneyEffect.text = DisplayEffect(gameEvent.MoneyDelta) + gameEvent.MoneyDelta;
        greenPointsEffect.text = DisplayEffect(gameEvent.GreenPointDelta) + gameEvent.GreenPointDelta;
        happinessEffect.text = DisplayEffect(gameEvent.HappinessDelta) + gameEvent.HappinessDelta;

        buildingDestroyerPopupPanel.SetActive(true);
    }

    public void RemovePopup()
    {
        Game.GameEvent.TileDelta(Game.Tiles, true);
        eventPopupPanel.SetActive(false);
    }

    public void RemoveDestroyerPopup()
    {
        Game.GameEvent.TileDelta(Game.Tiles, true);
        buildingDestroyerPopupPanel.SetActive(false);
    }

    public void FixBuildings()
    {
        game.GameEvent.TileDelta(game.Tiles, false);
        buildingDestroyerPopupPanel.SetActive(false);
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


