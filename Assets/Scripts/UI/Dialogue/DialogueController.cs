using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController: MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTextDisplay;

    private bool isDialoguePlaying;

    private static DialogueController instance;
    private void Awake()
    {
        if (instance)
            Debug.LogWarning("Found multiple Dialogue Controllers in the scene.");

        instance = this;
    }

    private void Start()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
    }

    public void EnterDialogueMode()
    {
        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);
    }

    public void ExitDialogueMode()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
    }
}
