using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TP2 - Florencia Pak
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    //Variables para la caja de dialogo de la UI
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private GameObject player;
    private GameObject speaker;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;
    private int lineIndex;

    public float typingSpeed = 0.01f;

    public Animator anim;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        isDialogueActive = false;
    }

    void Update()
    {
        //if(player != null && speaker != null)
        //{
          //  TriggerDialogueExit(player, speaker);
        //}

        if (Input.GetKeyDown(KeyCode.F) && isDialogueActive)
        {
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        anim.Play("show");
        lines.Clear();

        foreach(DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if(lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLines)
    {
        dialogueArea.text = "";
        foreach(char letter in dialogueLines.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        anim.Play("hide");
    }

    IEnumerator TriggerDialogueExit()
    {
        yield return new WaitForSeconds(3f);
        EndDialogue();
        
    }

    public void TriggerExitAssignation(GameObject player, GameObject speaker)
    {
        this.player = player;
        this.speaker = speaker;
        StartCoroutine(TriggerDialogueExit());

    }
}
