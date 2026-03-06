using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OpeningCutscene : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;                    // Dialog box panel
    public TextMeshProUGUI nameText;            // Name display
    public TextMeshProUGUI dialogText;          // Dialog display
    public GameObject portraitImage;            // Portrait GameObject

    [Header("Data")]
    public TextAsset dialogJSON;                // JSON file

    private DialogDatabase dialogDatabase;
    private Coroutine dialogCoroutine;

    // THIS IS THE FUNCTION YOU CALL FROM TIMELINE SIGNAL
    public void PlayOpeningDialog(string cutscenename)
    {
        dialogDatabase = JsonUtility.FromJson<DialogDatabase>(dialogJSON.text);

        // Make sure panel is hidden at start
        if (panel != null)
            panel.SetActive(false);

        if (dialogCoroutine != null)
            StopCoroutine(dialogCoroutine);

        dialogCoroutine = StartCoroutine(PlayDialog(cutscenename));
    }

    private IEnumerator PlayDialog(string conversationID)
    {
        Debug.Log("Looking for: " + conversationID);

        // Get the conversation lines
        DialogLine[] lines = GetConversation(conversationID);

        if (lines == null || lines.Length == 0)
        {
            Debug.LogError($"Conversation '{conversationID}' not found or empty!");
            yield break;
        }

        // Enable panel at start
        if (panel != null)
            panel.SetActive(true);

        // Play each line
        foreach (DialogLine line in lines)
        {
            // Set name
            if (nameText != null)
                nameText.text = line.name;
            // Set portrait image
            if (portraitImage != null)
            {
                Debug.Log("Loading portrait: " + line.portrait);

                Sprite portraitSprite = Resources.Load<Sprite>(line.portrait);

                if (portraitSprite != null)
                {
                    portraitImage.GetComponent<Image>().sprite = portraitSprite;
                    Debug.Log("Portrait loaded successfully!");
                }
                else
                {
                    Debug.LogWarning($"Portrait '{line.portrait}' not found in Resources!");
                }
            }

            // Typewriter effect for dialog
            yield return StartCoroutine(TypewriterEffect(line.dialog, line.duration));
        }

        // Hide panel after all lines finish
        if (panel != null)
            panel.SetActive(false);

        // Clear text
        if (dialogText != null)
            dialogText.text = "";
        if (nameText != null)
            nameText.text = "";
    }

    private IEnumerator TypewriterEffect(string fullText, float duration)
    {
        if (dialogText == null)
            yield break;

        dialogText.text = "";

        // Typewriter speed - fixed at 2.5 seconds regardless of text length
        float typewriterDuration = 2.5f;
        float timePerCharacter = typewriterDuration / fullText.Length;

        // Type out the text
        foreach (char c in fullText)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(timePerCharacter);
        }

        // Wait for remaining time so player can read
        float remainingTime = duration - typewriterDuration;

        if (remainingTime > 0)
        {
            yield return new WaitForSeconds(remainingTime);
        }
    }

    private DialogLine[] GetConversation(string conversationID)
    {
        if (dialogDatabase == null || dialogDatabase.conversations == null)
        {
            Debug.LogError("DialogDatabase is null!");
            return null;
        }

        // Search through the list to find matching conversation
        foreach (var conversation in dialogDatabase.conversations)
        {
            if (conversation.id == conversationID)
            {
                return conversation.lines;
            }
        }

        Debug.LogError($"Conversation ID '{conversationID}' not found in database!");
        return null;
    }
}

// Data classes for JSON parsing
[System.Serializable]
public class DialogDatabase
{
    public List<Conversation> conversations;
}

[System.Serializable]
public class Conversation
{
    public string id;
    public DialogLine[] lines;
}

[System.Serializable]
public class DialogLine
{
    public string name;
    public string dialog;
    public float duration;
    public string portrait;
}