using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public AItem[] itemList;

    AItem GetItem(int index)
    {
        if (itemList[index] != null)
        {
            return itemList[index];
        }
        else
        {
            return null;
        }
    }

    public void AddItem(AItem item)
    {

    }
}