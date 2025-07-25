using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectSO", menuName = "Scriptable Objects/CardEffectSO")]
public abstract class CardEffectSO : ScriptableObject
{
    public abstract void ApplyEffect(CardRuntimeContext context);
}

