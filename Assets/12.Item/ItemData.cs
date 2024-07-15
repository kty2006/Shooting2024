using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    public Item[] Items;
    public void ItemsPoolAdd()
    {
        foreach (var item in Items)
        {
            ObjectPool.Instance.AddPool(item.tag);
        }
    }
}
