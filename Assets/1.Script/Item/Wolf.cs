using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Item
{
    public override void Init()
    {
        ss.headStatus1 = headSprites[0];
        ss.headStatus2 = headSprites[1];
        ss.headInven = headSprites[2];
        ss.headItem = headSprites[3];
        ss.skill1 = skillSprites[0];
        ss.Skill2 = null;
        ss.it = ItemType.Head;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        isHead = true;
        GetComponent<SpriteRenderer>().sprite = ss.headItem;
        //  skulJson = FindObjectOfType<SkulData>();
    }
}
