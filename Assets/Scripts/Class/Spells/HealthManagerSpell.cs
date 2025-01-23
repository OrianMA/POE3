using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthManager", menuName = "Spell/Health/HealthManager", order = 2)]
public class HealthManagerSpell : ASpell
{
    public float maxHealth = 0;
    public float health = 0;
    public float regen = 0;

    public void Init(AEntity origin)
    {
        getMaxHealth(origin);
        health = maxHealth;

    }

    public override void Tick(float Delta)
    {

    }

    public override void Use(AEntity origin, AEntity Target, System.Object[] argV)
    {
        getMaxHealth(origin);
        SetRegen(origin);
        GetCurrentHealth(); 

    }

    public void getMaxHealth(AEntity origin)
    {
        maxHealth = 0;
        
        foreach (ASpell spell in origin.effect)
        {
            if ((StatSpell)spell)
            {
                StatSpell statSpell = (StatSpell)spell;
                if (statSpell.statTypeAndValue.stat == EStats.MaxHealth)
                {
                    maxHealth += statSpell.statTypeAndValue.value;
                }
            }
        }
    }    

    void SetRegen(AEntity origin)
    {
        if (health < maxHealth)
        {
            regen = 0;
            foreach (ASpell spell in origin.effect)
            {
                if ((StatSpell)spell)
                {
                    StatSpell statSpell = (StatSpell)spell;
                    if (statSpell.statTypeAndValue.stat == EStats.HealthRegeneration)
                    {
                        regen += statSpell.statTypeAndValue.value;
                    }
                }
            }

            health += regen * Time.deltaTime;
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void  ApplyDamage(float dmg)
    {
        health -= dmg;

        if (health < 0)
        {
            GameObject loot = ItemGeneratorManager.Instance.GenerateItem();
            
        }
            
    }
    public float GetCurrentHealth()
    {
        return Mathf.Floor(health);
    }

}
