using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Based off: https://www.youtube.com/watch?v=_nRzoTzeyxU
public class DialogueManager : MonoBehaviour
{
    private Queue<string> conversation;

    public GameObject continueButton;
    public GameObject startButton;
    public GameObject overlay;

    public int tutorialStage = 0;
    public GameObject arrowImage;
    public GameObject goldArrow;
    public GameObject greenArrow;
    public GameObject happiArrow;
    public GameObject turnsArrow;

    public GameObject endTurnButton;
    public GameObject turnGenerationArrow;
    public GameObject endTurnsArrow;

    public GameObject plains;
    public GameObject mountains;
    public GameObject desert;
    public GameObject water;

    public static bool finishTutorial = false;

    public TextMeshProUGUI continueText;
    public GameObject shop;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    //public Animator boxAnimator;
    //public Animator avatarAnimator;

    private void Start()
    {
        conversation = new Queue<string>();
        //boxAnimator.SetBool("isOpen", true);
        //avatarAnimator.SetBool("isOpen", true);

    }

    public void StartDialogue(Dialogue dialogue)
    {

        Debug.Log("Start convo");

        nameText.text = dialogue.npcName;

        conversation.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            conversation.Enqueue(sentence);
        }


        DisplayNextSentence();

        startButton.SetActive(false);
        continueButton.SetActive(true);

    }

    // Display the text of the dialogue slowly
    IEnumerator GenerateSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char digit in sentence.ToCharArray())
        {
            dialogueText.text += digit;
            yield return null;
        }
    }


    public void DisplayNextSentence()
    {
        if (conversation.Count == 0 && tutorialStage != 6)
        {
            EndDialogue();
            return;
        }


        switch (tutorialStage)
        {

            case 2:
                endTurnButton.GetComponent<Button>().interactable = false;
                switch (conversation.Count)
                {
                    case 12:
                        arrowImage.SetActive(true);
                        break;

                    case 10:
                        arrowImage.SetActive(false);
                        goldArrow.SetActive(true);
                        break;

                    case 8:
                        goldArrow.SetActive(false);
                        greenArrow.SetActive(true);
                        break;

                    case 5:
                        greenArrow.SetActive(false);
                        happiArrow.SetActive(true);
                        break;

                    case 3:
                        happiArrow.SetActive(false);
                        turnsArrow.SetActive(true);
                        break;

                    default:
                        break;
                }
                break;

            case 3:
                endTurnButton.GetComponent<Button>().interactable = false;
                switch (conversation.Count)
                {
                    case 5:
                        plains.SetActive(true);
                        break;

                    case 4:
                        desert.SetActive(true);
                        break;

                    case 3:
                        water.SetActive(true);
                        break;

                    case 2:
                        mountains.SetActive(true);
                        break;

                    default:
                        break;
                }
                break;

            case 4:
                endTurnButton.GetComponent<Button>().interactable = false;
                switch (conversation.Count)
                {
                    case 1:
                    continueButton.SetActive(false);
                    break;
                }
                break;

            case 5:
                shop.SetActive(false);
                endTurnButton.GetComponent<Button>().interactable = true;
                switch(conversation.Count)
                {
                    case 2:
                        turnGenerationArrow.SetActive(true);
                        break;

                    case 1:
                        turnGenerationArrow.SetActive(false);
                        endTurnsArrow.SetActive(true);
                        continueButton.SetActive(false);
                        break;
                }
                break;

            case 6:
                overlay.SetActive(true);
                switch(conversation.Count)
                {
                    case 3:
                        goldArrow.SetActive(true);
                        happiArrow.SetActive(true);
                        greenArrow.SetActive(true);
                        break;

                    case 2:
                        goldArrow.SetActive(false);
                        happiArrow.SetActive(false);
                        greenArrow.SetActive(false);
                        break;

                    case 1:
                        continueText.text = "End Tutorial";
                        break;

                    case 0:
                        Debug.Log("GONNA FINISH");
                        finishTutorial = true;
                        break;

                }
                break;

        }


        string sentence = conversation.Dequeue();
        // Make sure that if the user clicks "next", the last
        // animation terminates
        StopAllCoroutines(); 
        StartCoroutine(GenerateSentence(sentence));

    }

    private IEnumerator WaitForAnimation (Animator animator)
    {
        do
        {
            yield return null;
        } while (animator.isActiveAndEnabled);
    }

    public void EndDialogue()
    {
        Debug.Log("Ended convo");
        //boxAnimator.SetBool("isOpen", false);
        //avatarAnimator.SetBool("isOpen", false);


        TutorialController.Instance.tutorialIndex++;

        overlay.SetActive(false);
    }


}
