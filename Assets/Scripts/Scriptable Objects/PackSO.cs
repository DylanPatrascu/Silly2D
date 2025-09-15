using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PackSO", menuName = "Scriptable Objects/PackSO")]
public class PackSO : ScriptableObject
{
    [HideInInspector] public string guid; // Unique identifier for saving/loading

    public Sprite sprite;
    public string title;
    public CardSO[] cards;
    public int numCards;

    // fixed number of common, uncommon, rare, with the chance for a mythic to replace a rare
    public int numCommon;
    public int numUncommon;
    public int numRare;

    public float mythicOdds;

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
