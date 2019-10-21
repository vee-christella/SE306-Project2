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

    public GameObject yesButton;
    public GameObject closeButton;
    public GameObject noButton;

    public float costToRepair;

    private Game game;

    public TextMeshProUGUI eventInfo;
    public TextMeshProUGUI moneyEffect;
    public TextMeshProUGUI greenPointsEffect;
    public TextMeshProUGUI happinesEffect;
    public TextMeshProUGUI costToRepairEffect;
    public TextMeshProUGUI eventTitle;
    public TextMeshProUGUI impact;

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

            if (costToRepair == 0)
            {
                impact.text = "You were lucky and none of your buildings were destroyed.";
                buildingDestroyedPanel.SetActive(false);
                closeButton.SetActive(true);
            }
            else
            {

                impact.text = "Some of your buildings have been destroyed by this event!";


                if (game.Money < costToRepair || (game.Money + game.MoneyDelta < 0))
                {

                    yesButton.GetComponent<Button>().interactable = false;

                }
                else
                {
                    yesButton.SetActive(true);
                    noButton.SetActive(true);
                    yesButton.GetComponent<Button>().interactable = true;

                }

                closeButton.SetActive(false);
                buildingDestroyedPanel.SetActive(true);
            }
        }
        else
        {
            impact.text = gameEvent.TileDeltaDesc;
            closeButton.gameObject.SetActive(true);
            buildingDestroyedPanel.SetActive(false);
        }

        eventInfo.text = gameEvent.Description;

        moneyEffect.text = DisplayEffect(gameEvent.MoneyDelta) + gameEvent.MoneyDelta;
        greenPointsEffect.text = DisplayEffect(gameEvent.GreenPointDelta) + gameEvent.GreenPointDelta;
        happinesEffect.text = DisplayEffect(gameEvent.HappinessDelta) + gameEvent.HappinessDelta;
        costToRepairEffect.text = costToRepair.ToString();
        eventTitle.text = gameEvent.Title + " has occurred!";

        eventPopupPanel.SetActive(true);
    }

    public void RemovePopup()
    {
        Game.GameEvent.TileDelta(Game.Tiles, true);
        closeButton.SetActive(false);
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


