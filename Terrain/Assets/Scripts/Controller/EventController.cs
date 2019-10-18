using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EventController : MonoBehaviour
{

    public GameObject eventPopupPanel;
    public GameObject buildingDestroyedPanel;

    private GameController gameController;

    public Button yesButton;
    public Button closeButton;

    public float costToRepair;

    private Game game;

    public TextMeshProUGUI eventInfo;
    public TextMeshProUGUI moneyEffect;
    public TextMeshProUGUI greenPointsEffect;
    public TextMeshProUGUI happinessEffect;
    public TextMeshProUGUI costToRepairEffect;

    public Game Game { get => game; set => game = value; }
    public GameController GameController { get => gameController; set => gameController = value; }

    public EventController() {

    }

    public void DisplayEventPopup()
    {
        // cost to repair is 20%  of building costs 
        Event gameEvent = Game.GameEvent;

        if (gameEvent.Type == Event.EventType.BuildingDestroyer)
        {
            costToRepair = Mathf.Abs(Game.GameEvent.CalculateCostToRepair(game.Tiles));
            if (game.Money < costToRepair || (game.Money + game.MoneyDelta < 0))
            {
                yesButton.interactable = false;
            }
            else
            {
                yesButton.interactable = true;
            }

            buildingDestroyedPanel.SetActive(true);
        }
        else
        {
            closeButton.gameObject.SetActive(true);
            buildingDestroyedPanel.SetActive(false);
        }

        eventInfo.text = "A  " + gameEvent.GetType().Name.ToString() + " has occurred! \n" + gameEvent.Description;

        moneyEffect.text = DisplayEffect(gameEvent.MoneyDelta) + gameEvent.MoneyDelta;
        greenPointsEffect.text = DisplayEffect(gameEvent.GreenPointDelta) + gameEvent.GreenPointDelta;
        happinessEffect.text = DisplayEffect(gameEvent.HappinessDelta) + gameEvent.HappinessDelta;
        costToRepairEffect.text = costToRepair.ToString();

        eventPopupPanel.SetActive(true);
    }

    public void RemovePopup()
    {
        Game.GameEvent.TileDelta(Game.Tiles, true);
        closeButton.gameObject.SetActive(false);
        eventPopupPanel.SetActive(false);
    }

    public void RemoveDestroyerPopup()
    {
        game.GameEvent.TileDelta(Game.Tiles, true);
        GameController.SetDelta(game.MoneyDelta, game.GreenDelta, game.GenerateHappiness);
        eventPopupPanel.SetActive(false);
    }

    public void FixBuildings()
    {
        game.Money = game.Money - costToRepair;
        game.GameEvent.TileDelta(game.Tiles, false);
        gameController.SetMetrics(game.Money, game.Green, game.Happiness);
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


