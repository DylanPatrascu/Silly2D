using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckSO", menuName = "Scriptable Objects/DeckSO")]
public class DeckSO : ScriptableObject
{
    public string title;
    public int maxCopies = 2;
    public int deckSize = 30;
    public List<CardSO> deckList = new List<CardSO>();
    
}
