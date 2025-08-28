using UnityEngine;

[CreateAssetMenu(fileName = "DeckDatabaseSO", menuName = "Scriptable Objects/DeckDatabaseSO")]
public class DeckDatabaseSO : ScriptableObject
{
    public DeckSO[] decks;
}
