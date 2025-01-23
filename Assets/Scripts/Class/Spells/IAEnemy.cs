using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Enemy/IAEnemy", order = 1)]
public class IAEnemy : ASpell
{
    public ASpell movement;
    public int speed;
    public List<ASpellAttack> attackSpells;

    [Header("Attack")]
    public float attackDelay;

    public AEntity owner;
    public AEntity target;
    public object[] args;
    
    public OnEnemyMove enemyMoveState = new();
    public OnEnemyAttack enemyAttackState = new();
    StateBase currentState;

    //Attack cache
    public List<float> attacksRange = new();
    public int attackIndex;

    public override void Tick(float delta)
    {
        currentState.UpdateState();
    }

    public override void Use(AEntity origin, AEntity _traget, object[] argV)
    {
        owner = origin;
        target = _traget;
        args = new object[] {
            target.transform.position,
            speed,
            argV[0]
                };

        attacksRange.Clear();

        foreach (ASpellAttack attack in attackSpells)
        {
            attacksRange.Add(attack.range);
        }
        attacksRange.Sort();

        ChangeState(enemyMoveState);
    }
    public void ChangeState(StateBase newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter(new object[]
        {
            this
        });
    }
}