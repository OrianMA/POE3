using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OnEnemyMove : StateBase
{
    private IAEnemy enemy;
    Vector3 targetLook;
    NavMeshAgent agent;
    public override void OnEnter(object[] obj)
    {
        enemy = (IAEnemy)obj[0];
        agent = (NavMeshAgent)enemy.args[2];
    }
    public override void UpdateState()
    {
        enemy.args[0] = enemy.target.transform.position;
        enemy.movement.Use(enemy.owner, enemy.target, enemy.args);
        targetLook = enemy.target.transform.position;
        targetLook.y = 0;
        enemy.owner.transform.LookAt(targetLook);
        for (int i = 0; i < enemy.attacksRange.Count; i++)
        {
            if (enemy.attacksRange[i] >= Vector3.Distance(enemy.owner.transform.position, enemy.target.transform.position))
            {
                enemy.attackIndex = i;
                enemy.ChangeState(enemy.enemyAttackState);
            }
        }
    }

    public override void OnExit()
    {
        agent.ResetPath();
        enemy.owner.visual.GetComponent<Animator>().SetBool("Idle", true);
    }
        
    public override void OnHurt()
    {
    }

}
