using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CretanBull : Item
{
    public override void Init()
    {
        id.Inven = itemSprites[0];
        id.passive = itemSprites[1];
        id.item = itemSprites[2];
        id.it = ItemType.Item;
    }

    void Start()
    {
        Init();
        GetComponent<SpriteRenderer>().sprite = id.item;
    }
}
