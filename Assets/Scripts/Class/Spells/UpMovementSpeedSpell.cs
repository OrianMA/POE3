using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Boost/MoveSpeedBoost", order = 1)]
public class UpMovementSpeedSpell : ASpell
{
    public float boostSpeedTime;
    public int boostValue;
    public float actualTime = 0;
    private StatSpell speedBoost;
    private AEntity _origin;

    public override void Tick(float delta)
    {
        if(_origin == null) return;
        
        if(actualTime < 0)
        {
            _origin.effect.Remove(speedBoost); 
        }
        else
        {
            actualTime -= delta;
            
        }
    }

    public override void Use(AEntity origin, AEntity traget, object[] argV)
    {        
        if (actualTime > 0) return;

        /*speedBoost = new StatSpell(EStats.MovementSpeed, boostValue);*/

        speedBoost = (StatSpell)SpellManager.Instance.GetSpell(SpellLink.SpellKey.statSpell);
        speedBoost.statTypeAndValue.stat = EStats.MovementSpeed;
        speedBoost.statTypeAndValue.value = boostValue;
        
        _origin = origin;
        origin.effect.Add(speedBoost);

        actualTime = boostSpeedTime;
    }
}
