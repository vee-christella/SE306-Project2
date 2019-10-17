using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {

        // Set the game map to generate level 1
        PlayerPrefs.SetInt("Level", 1);

        SceneManager.LoadScene("3D-GameScene");

    }

    public void Help()
    {

        // Set the game map to generate the tutorial
        PlayerPrefs.SetInt("Level", 0);

        SceneManager.LoadScene("TutorialScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
