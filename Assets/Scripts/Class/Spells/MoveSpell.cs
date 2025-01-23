using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Move/BasicMove", order = 1)]
public class MoveSpell : ASpell
{
    private AEntity m_Entity;
    public override void Tick(float delta)
    {
        
    }

    public override void Use(AEntity origin, AEntity traget, System.Object[] argV)
    {
        NavMeshAgent agent;
        m_Entity = origin;        
        GameObject visual = origin.visual;

        if (argV[2] != null)
        {
            agent = (NavMeshAgent)argV[2];
            agent.speed = (int)argV[1];
            agent.ResetPath();
            agent.SetDestination((Vector3)argV[0]);

            visual.GetComponent<Animator>().SetBool("Idle",false);
            visual.GetComponent<Animator>().SetTrigger("Walk");

        }
    }
}
