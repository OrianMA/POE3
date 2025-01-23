using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player refs")]
    /*public GameObject prefabStatInfo;
    public GameObject parentStat;*/
    public Slider lifeSlider;

    [Header("TargetLife refs")]
    public GameObject targetPanel;
    public Slider targetLifeSlider;
    public TextMeshProUGUI targetName;

    [Header("loot refs")]
    public GameObject lootPanel;
    public TextMeshProUGUI lootItemName;
    public TextMeshProUGUI lootEffects;

    public void UpdateStatPanel(List<ASpell> effects)
    {
        
    }

    public void UpdateLifeSlider(HealthManagerSpell hMP) 
    {
        lifeSlider.maxValue = hMP.maxHealth;
        lifeSlider.value = hMP.GetCurrentHealth();
    }  

    public void ShowTargetLife(AEntity target)
    {
        if (target != null)
        {
            targetPanel.SetActive(true);
            targetName.text = target.name;            
            foreach (KeyValuePair<SpellLink.SpellKey, ASpell> spell in target.usableSpell)
            {
                
                if (spell.Value is HealthManagerSpell)
                {
                    HealthManagerSpell managerspell = (HealthManagerSpell)spell.Value;
                    targetLifeSlider.maxValue = managerspell.maxHealth;
                    targetLifeSlider.value = managerspell.GetCurrentHealth();
                }
            }
        }
        else
        {
            targetPanel.SetActive(false);
        }
    }

    public void ShowLootOnMouse(LootScript loot) 
    { 
        if (loot != null) 
        {
            lootPanel.SetActive(true);
            lootPanel.transform.position = Input.mousePosition;
            lootItemName.text = loot.itemData.name;
            string textEffects = "";

            foreach(PairStatValue stat in loot.itemData.bonusStats)
            {
                textEffects += stat.stat.ToString() + " : " + stat.value +  "\n";
            }

            lootEffects.text = textEffects;
        }
        else
        {
            lootPanel.SetActive(false);
        }
    }
}
