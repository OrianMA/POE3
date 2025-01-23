using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASpell : ScriptableObject
{
    [SerializeField] public tags _tags;
    [SerializeField] public string _name;
    [SerializeField] public string _description;

    [Flags]
    public enum tags
    {
        None = 0,
        Buff = 2,
        Debuff = 4,
        DOT = 8,
        SkillShot = 16,
        Movement = 32,
    }

    public abstract void Use(AEntity origin, AEntity traget, System.Object[] argV);
    public abstract void Tick(float delta);
}
