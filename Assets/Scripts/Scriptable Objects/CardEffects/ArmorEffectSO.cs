using UnityEngine;

[CreateAssetMenu(fileName = "ArmorEffectSO", menuName = "Scriptable Objects/ArmorEffectSO")]
public class ArmorEffectSO : CardEffectSO
{
    public int armorAmount;

    public override string ApplyEffect(CardRuntimeContext context)
    {
        return context.caster.AddArmor(armorAmount);
    }
}