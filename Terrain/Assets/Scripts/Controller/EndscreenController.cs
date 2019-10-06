using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndscreenController : MonoBehaviour
{
    public GameObject endScreenUI;

    public TextMeshProUGUI endText;

    void Update()
    {
        Debug.Log("...." + GameController.Instance.Game.CurrentTurn);
        Debug.Log("----" + GameController.Instance.Game.MaxTurns);

        if (GameController.Instance.Game.CurrentTurn > GameController.Instance.Game.MaxTurns)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        endScreenUI.SetActive(true);

        if (GameController.Instance.Game.Green >= 1000) {
            endText.text = "YOU WIN! GREEN POINT GOAL REACHED.";
        } else {
            endText.text = "YOU LOSE! NOT ENOUGH GREEN POINTS. THE WORLD IS RUINED!";
        }
    }
}