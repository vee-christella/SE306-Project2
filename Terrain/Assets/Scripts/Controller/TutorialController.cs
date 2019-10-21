using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public GameObject[] tutorialStages;
    public int tutorialIndex;
    public TutorialController instance;

    public bool shopClosed = false;
    public bool buildingSold = false;
    public bool coalMineBuilt = false;
    public GameObject shop;

    public static TutorialController Instance { get; protected set; }
    Animator animator;


    private void Start()
    {
        Instance = this;
        animator = shop.GetComponent<Animator>();
    }

    private void Update()
    {

        //Debug.Log("Tutorial Index: " + tutorialIndex);
            //Debug.Log("Index is " + tutorialIndex + " and i is " + i);
            tutorialStages[tutorialIndex].SetActive(true);

            if (tutorialIndex > 0)
            {
                tutorialStages[tutorialIndex - 1].SetActive(false);
            }


       
       

        switch (tutorialIndex)
        {
            case 3:

                // Check if shop is open
                if (animator.GetBool("open"))
                {
                    tutorialIndex++;
                    Debug.Log("Tutorial 4 - complete!!");
                }
                break;
            case 4: 

                // Check if coal mine is built
                if (coalMineBuilt)
                {
                    //shop.SetActive(false);
                    tutorialIndex++;
                    Debug.Log("Tutorial 5 - complete!!");
                }
                break;

            case 5:

                // Check if shop is closed
                if (shopClosed)
                {
                    tutorialIndex++;
                    Debug.Log("Tutorial 6 - complete!!");
                }
                break;

            case 6:

                // Check if tooltip has been clicked
                if (MouseController.Instance.toolTip.activeSelf)
                {
                    tutorialIndex++;
                    Debug.Log("Tutorial 7 - complete!!");
                }
                break;

            case 7:

                // Check if building has been sold
                if (buildingSold)
                {
                    tutorialIndex++;
                    Debug.Log("Tutorial 8 - complete!!");
                }
                break;

            case 8: 
                if(GameController.Instance.Game.CurrentTurn == 1)
                {
                    tutorialIndex++;
                    Debug.Log("Tutorial 6 - complete!");
                }
                break;

            case 9: 
                if (DialogueManager.finishTutorial)
                {
                    SceneManager.LoadScene("MenuScene");
                }
                break;



        }

        }

        public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ShopClosed()
    {
        shopClosed = true;
    }

    public void BuildingSold()
    {
        buildingSold = true;
    }

}
