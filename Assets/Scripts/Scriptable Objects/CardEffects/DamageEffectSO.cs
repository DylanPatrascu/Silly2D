using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffectSO", menuName = "Scriptable Objects/DamageEffectSO")]
public class DamageEffectSO : CardEffectSO
{
    public int amount;
    public bool piercing;

    public override void ApplyEffect(CardRuntimeContext context)
    {
        context.target.TakeDamage(amount, piercing);
    }
}

