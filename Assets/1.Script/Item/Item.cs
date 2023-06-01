using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SkulStatus
{
    public Sprite headStatus1;
    public Sprite headStatus2;
    public Sprite headInven;
    public Sprite headItem;
    public Sprite skill1;
    public Sprite Skill2;
    public SkulData json;
    public int attack;
}
public struct ItemStatus
{
    public Sprite Inven;
    public Sprite passive;
    public Sprite item;
}

public abstract class Item : MonoBehaviour
{
    public SkulStatus ss = new SkulStatus();
    public ItemStatus id = new ItemStatus();
    public List<Sprite> headSprites = new List<Sprite>();
    public List<Sprite> skillSprites = new List<Sprite>();
    public List<Sprite> itemSprites = new List<Sprite>();

    public bool isHead { get; set; }
    public bool isItem { get; set; }
    public abstract void Init();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isHead)
        {
            ProjectManager.Instance.heads[1] = this;
            DataManager.Instance.playerData.nowPlayerData.playerdatajsons[0].head2 = gameObject.name;
            transform.gameObject.SetActive(false);
        }

        else if (collision.CompareTag("Player") && isItem)
        {
            ProjectManager.Instance.items.Add(this);
            DataManager.Instance.playerData.nowPlayerData.playerdatajsons[0].item0 = gameObject.name;
            transform.gameObject.SetActive(false);
        }
    }
}
