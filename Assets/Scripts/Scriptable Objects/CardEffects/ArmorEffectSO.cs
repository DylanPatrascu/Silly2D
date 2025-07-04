using UnityEngine;

[CreateAssetMenu(fileName = "ArmorEffectSO", menuName = "Scriptable Objects/ArmorEffectSO")]
public class ArmorEffectSO : CardEffectSO
{
    public int armorAmount;

    public override void ApplyEffect(CardRuntimeContext context)
    {
        context.caster.AddArmor(armorAmount);
    }
}