using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffectSO", menuName = "Scriptable Objects/DamageEffectSO")]
public class DamageEffectSO : CardEffectSO
{
    public int amount;
    public bool piercing;

    public override string ApplyEffect(CardRuntimeContext context)
    {
        return context.target.TakeDamage(amount, piercing);
    }
}

