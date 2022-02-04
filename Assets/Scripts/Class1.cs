using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Dialogue/Character")]

public class Character : ScriptableObject
{
    public string fullName;
    public Sprite[] portraits;
}
