using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject helpScreen;

    public void PlayTutorial()
    {
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene("3D-GameScene");
    }

    public void PlayEasyGame()
    {

        // Set the game map to generate level 1
        PlayerPrefs.SetInt("Level", 1);

        SceneManager.LoadScene("3D-GameScene");

    }

    public void PlayMediumGame()
    {

        // Set the game map to generate level 1
        PlayerPrefs.SetInt("Level", 2);

        SceneManager.LoadScene("3D-GameScene");

    }

    public void PlayHardGame()
    {

        // Set the game map to generate level 1
        PlayerPrefs.SetInt("Level", 3);

        SceneManager.LoadScene("3D-GameScene");

    }

    public void Help()
    {

        // Set the game map to show help button
        helpScreen.SetActive(true);
    }

    public void QuitHelp()
    {
        helpScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
