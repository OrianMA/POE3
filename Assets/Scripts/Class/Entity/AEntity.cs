using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SpellLink;
using static UnityEngine.UI.Image;

public class AEntity : MonoBehaviour, Ihealth
{
    public List<PairStatValue> baseStats;
    public SpellLink.SpellKey[] spellToGetOnAwake;
    //public ASpell[] usableSpell;
    public List<KeyValuePair<SpellLink.SpellKey, ASpell>> usableSpell;
    public List<ASpell> effect;
    public GameObject visual;
    protected NavMeshAgent _agent ;
    private Animator MyAnim;

    private void Awake()
    {
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent _agentNavMesh))
        {
            _agent = _agentNavMesh;
        }


        if (visual.TryGetComponent<Animator>(out Animator animator))
        {
            MyAnim = animator;
        }

        usableSpell = new List<KeyValuePair<SpellLink.SpellKey, ASpell>>();        

        foreach(SpellLink.SpellKey spellKey in spellToGetOnAwake)
        {
            usableSpell.Add(new KeyValuePair< SpellLink.SpellKey, ASpell > (spellKey, SpellManager.Instance.GetSpell(spellKey)));                    
        }
        
        foreach (PairStatValue statistique in baseStats)
        {
            StatSpell stat = (StatSpell)SpellManager.Instance.GetSpell(SpellLink.SpellKey.statSpell);
            stat.statTypeAndValue.stat = statistique.stat;
            stat.statTypeAndValue.value = statistique.value;
            effect.Add(stat);
        }
        foreach (KeyValuePair<SpellLink.SpellKey, ASpell> spell in usableSpell)
        {   
            if (spell.Value as HealthManagerSpell)
            {
                HealthManagerSpell hMP = (HealthManagerSpell)spell.Value;
                hMP.Init(this);
                break;
            }
        }

        
    }

    public int GetStats(EStats stat)
    {
        int statValue = 0;
        foreach (ASpell spell in effect)
        {         
            if((StatSpell)spell)
            {
                StatSpell statSpell = (StatSpell)spell;
                if (statSpell.statTypeAndValue.stat == stat)
                {
                    statValue += statSpell.statTypeAndValue.value;                 
                }
            }
        }
        return statValue;
    }
    public void Move(Vector3 destination)
    {
        System.Object[] tempArgV = new System.Object[3];
        tempArgV[0] = destination;
        tempArgV[1] = GetStats(EStats.MovementSpeed);

        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent nAgent))
        {
            tempArgV[2] = nAgent;
        }
        foreach(KeyValuePair<SpellLink.SpellKey,ASpell> spell in usableSpell)
        {
            if (spell.Key == SpellLink.SpellKey.Move)
            {
                spell.Value.Use(this, null, tempArgV);
            }
        }
    }
    public void TakeDamage(float dmg)
    {
        foreach (KeyValuePair<SpellLink.SpellKey, ASpell> spell in usableSpell)
        {
            if (spell.Key == SpellLink.SpellKey.HealthManager)
            {
                HealthManagerSpell managerspell = (HealthManagerSpell)spell.Value;
                managerspell.ApplyDamage(dmg);
                return;
            }
        }
    }

    public void Update()
    {
        if (_agent == null || MyAnim == null) return;

        #region Visual Player Rotation
        if(_agent.path.corners.Length>1)
        {
            Vector3 direction = (_agent.path.corners[1] - transform.position).normalized;
            visual.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        if (_agent.pathStatus == NavMeshPathStatus.PathComplete && _agent.remainingDistance == 0)
        {
            
            MyAnim.SetBool("Idle", true);
            //print(_agent.remainingDistance);
/*            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if ((!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f) && !MyAnim.GetBool("Idle"))
                {
                    MyAnim.SetBool("Idle", true);
                }
            }*/
        }
        else
        {
            
        }
        #endregion
        //print("Aentity.Update()");
    }
}
