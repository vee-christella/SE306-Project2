using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
This class controls the end game screen. This screen will either popup when the player has
no money left, or when the number of turns has reached the maximum. This screen will tell
the player whether she has won or not. The player will win if she has at least 1000 green 
points when the last turn is ended.
*/
public class EndscreenController : MonoBehaviour
{
    public GameObject endScreenUI;
    public TextMeshProUGUI endText;

    void Update()
    {
        if (GameController.Instance.Game.IsEnd)
        {
            EndGame();
        }
    }

    /*
    Pops up the end screen over the game.
    */
    public void EndGame()
    {
        endScreenUI.SetActive(true);

        // WIN - Player has ended with at least 1000 green points
        if (GameController.Instance.Game.IsVictory)
        {
            endText.text = "YOU WIN! Congratulations, Mayor! You have successfully completed the game. The choices you have made have positively affected the environment.";
        }
        // LOSE
        else
        {
            // Player had ended with less than 1000 green points
            if ((GameController.Instance.Game.CurrentTurn >= GameController.Instance.Game.MaxTurns) && (GameController.Instance.Game.Green < GameController.Instance.Game.MaxGreen))
            {
                endText.text = "YOU LOSE! Unfortunately you have not balanced profit with environmental effects. Remember that the buildings you build can negatively affect the environment.";
            }
            // Player has run out of money
            else
            {
                endText.text = "YOU LOSE! You have run out of money. A city needs money to run, and certain buildings generate money. The hard part is finding the blanace between profit and environmental effects";
            }
        }
    }
}