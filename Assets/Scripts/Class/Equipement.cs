using UnityEngine;

public class Equipement : MonoBehaviour
{
    public AItem[] equipementList;

    void GetItems()
    {
        for (int i = 0; i < equipementList.Length; i++)
        {
            if (equipementList[i] != null)
            {
                Debug.Log("Vous avez:" + equipementList[i]);
            }
        }
    }
}
