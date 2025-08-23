using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckSO", menuName = "Scriptable Objects/DeckSO")]
public class DeckSO : ScriptableObject
{
    public string title;
    public int maxCopies;
    public int deckSize;
    public List<CardSO> deckList = new List<CardSO>();
    
}
