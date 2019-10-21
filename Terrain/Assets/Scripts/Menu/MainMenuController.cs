using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{


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

        // Set the game map to generate the tutorial
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene("3D-GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
