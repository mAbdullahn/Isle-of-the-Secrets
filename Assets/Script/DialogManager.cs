using UnityEngine;
using TMPro; // Use 'using UnityEngine.UI;' if not using TextMeshPro

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameDisplay;
    public TextMeshProUGUI messageDisplay;

    // This is the function the Signal Receiver WILL see!
    public void PlayDialog(DialogData data)
    {
        nameDisplay.text = data.speakerName;
        messageDisplay.text = data.message;
    }
}