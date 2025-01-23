using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;




public class Player : AEntity
{
    Object inventory;

    public List<EquipItem> equipements;
    public List<UserSpell> PlayerSpells;
    public AEntity target;
    public UIManager uiManager;

    public EquipItem equipement;
    [HideInInspector]public LootScript lootOnMouse;
    
    void LevelUp()
    {

    }    


    public void UseSpell(PlayerController.input inputname)
    {
        //print(PlayerSpells.Count);

        foreach (UserSpell uS in PlayerSpells)
        {
            if(uS.input == inputname)
            {
                foreach (KeyValuePair<SpellLink.SpellKey, ASpell> spell in usableSpell)
                {
                    if (spell.Key == uS.Spell)
                    {
                        object[] tempArgV = new object[2];
                        tempArgV[0] = GetStats(EStats.Agility);
                        tempArgV[1] = GetStats(EStats.Strength);
                        spell.Value.Use(this, target, tempArgV);
                        return;
                    }
                }
            }

            
            
        }   
    }

    private new void Update()
    {
        base.Update();

        foreach(KeyValuePair<SpellLink.SpellKey, ASpell> spell in usableSpell)
        {
            spell.Value.Tick(Time.deltaTime); 
            
            if (spell.Value as HealthManagerSpell)
            {
                HealthManagerSpell hMP = (HealthManagerSpell)spell.Value;
                hMP.Use(this, null, null);
                uiManager.UpdateLifeSlider(hMP);
            }
        }
        

        foreach(ASpell spell in effect)
        {
            spell.Tick(Time.deltaTime);            
        }

        uiManager.ShowTargetLife(target);
        uiManager.ShowLootOnMouse(lootOnMouse);
    }

    public void EquipItem(EquipItem equipmnt)
    {
        equipements.Add(equipmnt);
        equipmnt.OnEquip(this);
    }
    
    

    

}

