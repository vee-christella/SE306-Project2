﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject achievementPanel;
    public GameObject optionPanel;
    public GameObject helpPanel;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        helpPanel.SetActive(false);
        achievementPanel.SetActive(false);
        optionPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MenuButton()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");

    }

}
