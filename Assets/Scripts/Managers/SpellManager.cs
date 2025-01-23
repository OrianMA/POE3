using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instance { get; private set; }

    public SpellLink[] allSpells;

    public void Awake()
    {
        Instance = this;
    }

    public ASpell GetSpell(SpellLink.SpellKey key)
    {
        foreach (SpellLink spellLink in allSpells)
        {
            if(spellLink.key == key) 
            {
                var newSpell = spellLink.spell.Clone();
                return newSpell; 
            }            
        }
        return null;
    }
}

[Serializable]
public struct SpellLink
{
    public SpellKey key;
    public ASpell spell;
    public enum SpellKey
    {        
        BasicAttack,
        Move,
        AOEAttack,
        DOTAttack,
        SkillShot,
        Heal,
        speedBoost,
        speedStat,
        HealthManager,
        statSpell,
        IAEnemy,
        LunarExplosion,
        ApplyDamage,
    }
}

public static class ScriptableObjectExtension
{
    /// <summary>
    /// Creates and returns a clone of any given scriptable object.
    /// </summary>
    public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
    {
        if (scriptableObject == null)
        {
            Debug.LogError($"ScriptableObject was null. Returning default {typeof(T)} object.");
            return (T)ScriptableObject.CreateInstance(typeof(T));
        }

        T instance = UnityEngine.Object.Instantiate(scriptableObject);
        instance.name = scriptableObject.name; // remove (Clone) from name
        return instance;
    }
}
