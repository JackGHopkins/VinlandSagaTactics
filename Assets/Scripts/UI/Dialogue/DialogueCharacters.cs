using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Dialogue/Character")]

public class DialogueCharacter : ScriptableObject
{
    public string fullName;
    public Sprite[] portraits;
}
