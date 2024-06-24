using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TP2 - Florencia Pak
[System.Serializable]
public class DialogueCharacter
{
    public string name;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)] public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public bool wasTriggered;
    public Dialogue dialogue;

     public void TriggerDialogue()
     {
         DialogueManager.Instance.StartDialogue(dialogue);
     }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !wasTriggered)
        {
            wasTriggered = true;
            TriggerDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DialogueManager.Instance.TriggerExitAssignation(other.gameObject,this.gameObject);
        }
    }
}