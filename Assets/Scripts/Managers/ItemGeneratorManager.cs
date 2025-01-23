using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemGeneratorManager : MonoBehaviour
{
    public static ItemGeneratorManager Instance { get; private set; }

    public GameObject lootPrefab;

    public void Awake()
    {
        Instance = this;
    }    

    public GameObject GenerateItem()
    {
        GameObject itemLoot = Instantiate(lootPrefab);

        if(itemLoot.TryGetComponent(out LootScript loot))
        {            
            loot.itemData = ScriptableObject.CreateInstance<EquipItem>();
            loot.itemData.Type = (EquipItem.EquipementType)typeof(EquipItem.EquipementType).GetRandomEnumValue();
            loot.itemData._rarity = (EquipItem.Rarity)typeof(EquipItem.Rarity).GetRandomEnumValue();

            int numberOfAffixes = UnityEngine.Random.Range(0, 6);

            List<PairStatValue> pairStats = new List<PairStatValue>();

            for(int i = 0; i <= numberOfAffixes; i++)
            {
                PairStatValue statValue = new PairStatValue();
                statValue.stat = (EStats)typeof(EStats).GetRandomEnumValue();
                int randomStatValue = UnityEngine.Random.Range(1, 20);
                statValue.value = randomStatValue;
                pairStats.Add(statValue);
            }

            loot.itemData.bonusStats = pairStats;

            loot.Init();
            return itemLoot;
        }

        return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject obj = GenerateItem();
            obj.transform.position = Vector3.zero;
        }
    }
}

public static class EnumExtensions
{
    public static Enum GetRandomEnumValue(this Type t)
    {
        return Enum.GetValues(t)          // get values from Type provided
            .OfType<Enum>()               // casts to Enum
            .OrderBy(e => Guid.NewGuid()) // mess with order of results
            .FirstOrDefault();            // take first item in result
    }
}
