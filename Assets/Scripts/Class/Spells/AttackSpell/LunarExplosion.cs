using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Attacks/LunarExplosion", order = 1)]
public class LunarExplosion : ASpellAttack
{
    public AOESpell aoeSpellPrefab;
    public SpellLink.SpellKey spellToApply;
    public ASpellAttack spellDamage;
    public override void Use(AEntity origin, AEntity target, object[] argV)
    {
        base.Use(origin, target, argV);
        AOESpell aoeSpell = Instantiate(aoeSpellPrefab);
        if (target == null)
        {
            aoeSpell.layerTarget = LayerMask.NameToLayer("Enemy");
        }else
            aoeSpell.layerTarget |= (1 << target.gameObject.layer);
        aoeSpell.transform.position = origin.transform.position;
        aoeSpell.origin = origin;
        ASpell spell = SpellManager.Instance.GetSpell(spellToApply);
        spellDamage = (ASpellAttack)spell;
        spellDamage.damage = damage;
        aoeSpell.spell = spellDamage;
        aoeSpell.aoeRange = range;

        aoeSpell.Init();
    }
    public override void Tick(float delta)
    {
        //base.Tick(delta);
    }
}
