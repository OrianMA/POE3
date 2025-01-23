using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpell : MonoBehaviour
{
    public ParticleSystem AOEParticle;
    public LayerMask layerTarget;
    public AEntity origin;
    public object[] spellArgs;
    public float aoeRange;
    public ASpell spell;

    public List<AEntity> entitiesAffect = new();
    public void Init()
    {
        Collider[] allHit;
        allHit = Physics.OverlapSphere(origin.transform.position, aoeRange, layerTarget);
        foreach (Collider hit in allHit)
        {
            print(hit.name);
            spell.Use(origin, hit.GetComponent<AEntity>(), spellArgs);
        }
    }
}
