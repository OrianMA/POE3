using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyAttack : StateBase
{
    private IAEnemy enemy;
    public override void OnEnter(object[] obj)
    {
        enemy = (IAEnemy)obj[0];
        enemy.owner.StartCoroutine(Attack());
    }
    public override void UpdateState()
    {
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(enemy.attackDelay);
        enemy.attackSpells[enemy.attackIndex].Use(enemy.owner, enemy.target, enemy.args);
        yield return new WaitForSeconds(enemy.attackSpells[enemy.attackIndex].spellDuration);
        if (enemy.attackSpells[enemy.attackIndex].range < Vector3.Distance(enemy.owner.transform.position, enemy.target.transform.position)) {
            enemy.ChangeState(enemy.enemyMoveState);
        } else
        {
            enemy.owner.StartCoroutine(Attack());
        }
    }

    public override void OnExit()
    {
    }

    public override void OnHurt()
    {

    }

}
