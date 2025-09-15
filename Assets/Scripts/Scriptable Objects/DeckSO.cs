using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DeckSO", menuName = "Scriptable Objects/DeckSO")]
public class DeckSO : ScriptableObject
{
    [HideInInspector] public string guid; // Unique identifier for saving/loading

    public string title;
    public int maxCopies = 2;
    public int deckSize = 20;
    public List<CardSO> deckList = new List<CardSO>();

    public IEnumerable<CardSO> GetAllCards()
    {
        return deckList;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(guid))
        {
            guid = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}
