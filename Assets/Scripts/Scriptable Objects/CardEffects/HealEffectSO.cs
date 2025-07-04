using UnityEngine;

[CreateAssetMenu(fileName = "HealEffectSO", menuName = "Scriptable Objects/HealEffectSO")]
public class HealEffectSO : CardEffectSO
{
    public int healAmount;

    public override void ApplyEffect(CardRuntimeContext context)
    {
        context.caster.Heal(healAmount);
    }
}