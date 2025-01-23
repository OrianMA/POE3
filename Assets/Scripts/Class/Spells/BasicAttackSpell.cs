using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Attacks/BasicAttacks", order = 1)]
public class BasicAttackSpell : ASpell
{
    public int rangeAttack;
    public float attackSpeed;

    private float timeBeforeAttack = 0;

    public override void Tick(float delta)
    {
        if (timeBeforeAttack > 0)
        {
            timeBeforeAttack -= delta;
        }
    }

    public override void Use(AEntity origin, AEntity target, object[] argV)
    {
        attackSpeed = 1 / (int)argV[0];

        if (timeBeforeAttack > 0) { return; }
        if(target == null) { return; }

        

        if (Vector3.Distance(origin.transform.position, target.transform.position) > rangeAttack) return;
        

        if (target.TryGetComponent<Ihealth>(out Ihealth targetIhealth))
        {
            Debug.Log("BasicAttack");
            targetIhealth.TakeDamage((int)argV[1]);
            timeBeforeAttack = attackSpeed;
        }
    }
}
