using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabaseSO", menuName = "Scriptable Objects/CardDatabaseSO")]
public class CardDatabaseSO : ScriptableObject
{
    public CardSO[] cards;
}
