using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : AItem, IEquipable
{
    public EquipementType Type;    
    public Rarity _rarity;
    public List<PairStatValue> bonusStats;

    private List<StatSpell> tempStats;

    private AEntity _Origin;
    public enum Rarity
    {
        Commun,        
        Rare,
        Legendary
    }

    public enum EquipementType
    {
        Head,
        Chest,
        Leg,
        weapon
    }

    public virtual void OnEquip(AEntity origin)
    {
        _Origin = origin;

        foreach(PairStatValue stat in bonusStats)
        {
            StatSpell actualStat = (StatSpell)SpellManager.Instance.GetSpell(SpellLink.SpellKey.statSpell);
            actualStat.statTypeAndValue.stat = stat.stat;
            actualStat.statTypeAndValue.value = stat.value;
            origin.effect.Add(actualStat);
            tempStats.Add(actualStat);
        }
    }

    public virtual void OnUnequip()
    {
        foreach(StatSpell stat in tempStats)
        {
            _Origin.effect.Remove(stat);
            tempStats.Remove(stat);
        }
    }
}
