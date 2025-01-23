using System;
using UnityEngine;

public enum EStats
{ 
    MaxHealth,
    Health,
    HealthRegeneration,
    MovementSpeed,
    Agility, 
    Strength,
    CriticalChance,
}

[Serializable]
public class PairStatValue
{
    public EStats stat;
    public int value;
}





