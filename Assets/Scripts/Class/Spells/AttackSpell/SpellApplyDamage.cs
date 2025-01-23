using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Attacks/ApplyDamage", order = 1)]
public class SpellApplyDamage : ASpellAttack
{
    public override void Use(AEntity origin, AEntity target, object[] argV)
    {
        base.Use(origin, target, argV);
        target.TakeDamage(damage);
    }
}
