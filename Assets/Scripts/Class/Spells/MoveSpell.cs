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
        Rigidbody rb;
        m_Entity = origin;        
        GameObject visual = origin.visual;

        if (argV[2] != null)
        {
            if (argV[2] is NavMeshAgent agent) // V�rification si argV[2] est un NavMeshAgent
            {
                agent.speed = (int)argV[1];
                agent.ResetPath();
                agent.SetDestination((Vector3)argV[0]);
            }
            else if (argV[2] is Rigidbody rigidbody) // V�rification si argV[2] est un Rigidbody
            {
                rb = rigidbody;
                rb.linearVelocity = (Vector3)argV[0] * (int)argV[1] * Time.deltaTime;
            }
            else
            {
                Debug.LogWarning($"Le type de argV[2] ({argV[2].GetType()}) n'est pas pris en charge.");
            }

            visual.GetComponent<Animator>().SetBool("Idle",false);
            visual.GetComponent<Animator>().SetTrigger("Walk");
        }
    }
}
