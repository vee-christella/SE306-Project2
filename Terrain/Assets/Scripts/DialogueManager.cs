using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> conversation;

    public GameObject continueButton;
    public GameObject startButton;
    public GameObject overlay;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private void Start()
    {
        conversation = new Queue<string>();
        animator.SetBool("isOpen", true);
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
        if (conversation.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = conversation.Dequeue();
        // Make sure that if the user clicks "next", the last
        // animation terminates
        StopAllCoroutines(); 
        StartCoroutine(GenerateSentence(sentence));

    }

    public void EndDialogue()
    {
        Debug.Log("Ended convo");
        animator.SetBool("isOpen", false);

        //overlay.SetActive(false);
    }


}
