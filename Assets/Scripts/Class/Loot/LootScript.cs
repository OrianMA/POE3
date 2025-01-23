using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public EquipItem itemData;
    public GameObject[] fx;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        fx[0].SetActive(false);
        fx[1].SetActive(false);
        fx[2].SetActive(false);

        switch(itemData._rarity)
        {
            case EquipItem.Rarity.Commun:
                fx[0].SetActive(true);
                break;

            case EquipItem.Rarity.Rare:
                fx[1].SetActive(true);
                break;

            case EquipItem.Rarity.Legendary:
                fx[2].SetActive(true);
                break;
        }
    }
}
