using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpeningCutsceneDialog : MonoBehaviour
{
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text dialog;
    [SerializeField] float typingSpeed = 0.03f;
    [SerializeField] Sprite[] potraitCollection;
    [SerializeField] GameObject Potrait;
    [SerializeField] GameObject dialogpanel;

    private Coroutine typingCoroutine;

    public void Line1()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[0];
        characterName.text = "Alex";
        StartTypewriter("The sea suits you, Alice. You look radiant today.");
    }
    public void Line2()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[3];
        characterName.text = "alice";
        StartTypewriter("Oh, stop it... You're just saying that.");
    }
    public void Line3()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[0];
        characterName.text = "alex";
        StartTypewriter("I mean it. I've traveled many places, but none compare to this moment... here, with you.");
    }
    public void Line4()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[3];
        characterName.text = "alice";
        StartTypewriter("You know... I've always dreamed of this. Sailing the open sea with you, my adventurer. (Giggles) Finally, we're doing it together!");
    }
    public void Line5()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[1];
        characterName.text = "alex";
        StartTypewriter("Just don't blame me if we get seasick!");
    }
    public void Line6()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[3];
        characterName.text = "alice";
        StartTypewriter("Hey! I'm tougher than I look!");
    }
    public void Line7()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[0];
        characterName.text = "alex";
        StartTypewriter("I know. That's why I love you.");
    }
    public void Line8()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[5];
        characterName.text = "alice";
        StartTypewriter("Alex... promise me one thing.No matter what happens... we'll always be together. Right?");
    }
    public void Line9()
    {
        dialogpanel.SetActive(true);
        Potrait.GetComponent<Image>().sprite = potraitCollection[0];
        characterName.text = "alex";
        StartTypewriter("I'll stand by your side, no matter what.");
    }

    private void StartTypewriter(string fullText)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    private IEnumerator TypeText(string fullText)
    {
        dialog.text = "";

        foreach (char c in fullText)
        {
            dialog.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void HideDialog()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialog.text = "";
        characterName.text = "";
    }
}
