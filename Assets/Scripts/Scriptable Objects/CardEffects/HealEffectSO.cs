using UnityEngine;

[CreateAssetMenu(fileName = "HealEffectSO", menuName = "Scriptable Objects/HealEffectSO")]
public class HealEffectSO : CardEffectSO
{
    public int healAmount;

    public override string ApplyEffect(CardRuntimeContext context)
    {
        return context.caster.Heal(healAmount);
    }
}