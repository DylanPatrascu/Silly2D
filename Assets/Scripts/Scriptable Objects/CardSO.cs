using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class CardSO : ScriptableObject
{
    [HideInInspector] public string guid; // Unique identifier for saving/loading

    public string title;
    public string description;
    public string flavour;
    public Sprite sprite;
    public Rarity rarity = Rarity.Common;
    public bool quickCast;

    public CardEffectSO[] effects;

#if UNITY_EDITOR
    // Ensure GUID is always set in editor
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
