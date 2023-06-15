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
        StartCoroutine(ItemDrop());
    }

    IEnumerator ItemDrop()
    {
        Rigidbody2D rigid = transform.gameObject.GetComponent<Rigidbody2D>();
        if (!MapManager.Instance.isHead)
        {
            rigid.AddForce(new Vector2(0, 1) * 300);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(1.2f);
        rigid.bodyType = RigidbodyType2D.Static;
    }
}
