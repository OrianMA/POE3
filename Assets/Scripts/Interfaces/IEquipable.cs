using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    public void OnEquip(AEntity origin);

    public void OnUnequip();
}
