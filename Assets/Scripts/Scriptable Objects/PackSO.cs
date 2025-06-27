using UnityEngine;

[CreateAssetMenu(fileName = "PackSO", menuName = "Scriptable Objects/PackSO")]
public class PackSO : ScriptableObject
{
    public Sprite sprite;
    public string title;
    public CardSO[] cards;
    public int numCards;

    //fixed number of common, uncommon, rare, with the chance for a mythic to replace a rare
    public int numCommon;
    public int numUncommon;
    public int NumRare;

    public float mythicOdds;
}
