using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASpellAttack : ASpell
{
    public float range;
    public int damage;
    public float spellDuration;

    public override void Use(AEntity origin, AEntity traget, object[] argV)
    {
        
    }

    public override void Tick(float delta)
    {
        
    }
}
