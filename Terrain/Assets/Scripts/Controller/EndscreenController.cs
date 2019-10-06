using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndscreenController : MonoBehaviour
{
    public GameObject endScreenUI;

    void Update()
    {
        Debug.Log("...." + GameController.Instance.Game.CurrentTurn);

        if (GameController.Instance.Game.CurrentTurn > GameController.Instance.Game.MaxTurns)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        endScreenUI.SetActive(true);
    }
}