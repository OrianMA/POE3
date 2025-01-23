using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : ASpell
{
    //public EItem items[];
    public override void Tick(float delta)
    {
        
    }

    public override void Use(AEntity origin, AEntity traget, System.Object[] argV)
    {
        Player player = origin is Player ? (Player) origin : null;
        if (player != null)
        {

        }
    }
}
