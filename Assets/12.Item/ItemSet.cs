using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSet : MonoBehaviour
{
    public ItemData ItemData;
    private void Start()
    {
        ItemData.ItemsPoolAdd();
    }
}
