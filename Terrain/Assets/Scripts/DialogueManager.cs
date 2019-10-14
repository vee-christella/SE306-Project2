using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> conversation;

    public GameObject continueButton;
    public GameObject startButton;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        conversation = new Queue<string>();
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


    public void DisplayNextSentence()
    {
        if (conversation.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = conversation.Dequeue();
        dialogueText.text = sentence;

    }

    public void EndDialogue()
    {
        Debug.Log("Ended convo");
    }
}
