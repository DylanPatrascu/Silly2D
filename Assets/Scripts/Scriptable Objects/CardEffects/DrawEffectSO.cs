using UnityEngine;

[CreateAssetMenu(fileName = "DrawEffectSO", menuName = "Scriptable Objects/DrawEffectSO")]
public class DrawEffectSO : CardEffectSO
{
    public int amount;

    public override string ApplyEffect(CardRuntimeContext context)
    {
        return context.caster.DrawCard(amount);
    }
}

