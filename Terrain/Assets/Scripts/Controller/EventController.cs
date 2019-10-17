using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventController : MonoBehaviour
{

    public GameObject eventPopupPanel;
    public GameObject buildingDestroyedPanel;

    private GameController gameController;

    public Button yesButton;
    public Button closeButton;
    private Game game;

    public TextMeshProUGUI eventInfo;
    public TextMeshProUGUI moneyEffect;
    public TextMeshProUGUI greenPointsEffect;
    public TextMeshProUGUI happinessEffect;

    public Game Game { get => game; set => game = value; }
    public GameController GameController { get => gameController; set => gameController = value; }

    public EventController() {
    }


    public void DisplayEventPopup()
    {
        Event gameEvent = Game.GameEvent;

        if (gameEvent.Type == Event.EventType.BuildingDestroyer)
        {
            if (game.Money < 200 || (game.Money + game.MoneyDelta < 200))
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

        eventInfo.text = "A  " + gameEvent.GetType().Name.ToString() + " has occurred! \n";

        moneyEffect.text = DisplayEffect(gameEvent.MoneyDelta) + gameEvent.MoneyDelta;
        greenPointsEffect.text = DisplayEffect(gameEvent.GreenPointDelta) + gameEvent.GreenPointDelta;
        happinessEffect.text = DisplayEffect(gameEvent.HappinessDelta) + gameEvent.HappinessDelta;

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
        Game.GameEvent.TileDelta(Game.Tiles, true);
        eventPopupPanel.SetActive(false);
    }

    public void FixBuildings()
    {
        game.Money = game.Money - 200;
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


