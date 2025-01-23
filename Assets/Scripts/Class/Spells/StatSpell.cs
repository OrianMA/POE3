using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats", order = 1)]
public class StatSpell : ASpell
{
    public PairStatValue statTypeAndValue = new PairStatValue();


    public override void Tick(float delta)
    {

    }

    public override void Use(AEntity origin,AEntity Target, System.Object[] argV)
    {
    }

}
