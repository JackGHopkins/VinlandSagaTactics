using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI nameDisplay;
    public GameObject portrait;
    public GameObject continueButton;
    public DialogueLine[] sentences;
    public float typingSpeed;

    private AudioSource dialogueAudio;
    private int index;

    void Start()
    {
        textDisplay.text = "";
        dialogueAudio = GetComponent<AudioSource>();
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index].sentence)
            continueButton.SetActive(true);
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentences()
    {
        dialogueAudio.Play();
        continueButton.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
}
