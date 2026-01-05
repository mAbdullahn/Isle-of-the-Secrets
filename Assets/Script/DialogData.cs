using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogLine", menuName = "RPG/Dialog Line")]
public class DialogData : ScriptableObject
{
    public string speakerName;
    [TextArea(3, 10)] // This makes a nice big box for the message
    public string message;
}