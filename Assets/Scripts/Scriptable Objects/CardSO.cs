using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class CardSO : ScriptableObject
{
    public string title;
    public string description;
    public string flavour;
    public Sprite sprite;
    public Rarity rarity = Rarity.Common;
    public bool quickCast;

    public CardEffectSO[] effects;
}
