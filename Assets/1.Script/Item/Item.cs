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
    public ItemType it;
    public GameObject obj;
    public string name;
}
public struct ItemStatus
{
    public Sprite Inven;
    public Sprite passive;
    public Sprite item;
    public ItemType it;
}

public enum ItemType
{
    None,
    Head,
    Essence,
    Item
}

public abstract class Item : MonoBehaviour
{
    public SkulStatus ss = new SkulStatus();
    public ItemStatus id = new ItemStatus();
    public ItemType it;
    public List<Sprite> headSprites = new List<Sprite>();
    public List<Sprite> skillSprites = new List<Sprite>();
    public List<Sprite> itemSprites = new List<Sprite>();
    public SkulData.Data skulJson;
    public ItemData.Data itemJson;
    public bool isHead { get; set; }
    public bool isItem { get; set; }
    public abstract void Init();


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && it == ItemType.Head)
        {
            if (ProjectManager.Instance.player.isPush)
            {
                ProjectManager.Instance.player.isPush = false;
                //ss.obj.SetActive(false);
                Instantiate(Resources.Load<Item>(string.Format($"Head/{ProjectManager.Instance.heads[0].name}")), MapManager.Instance.headTrans);
                ItemHead();
                Destroy(ss.obj);
                Debug.Log(ProjectManager.Instance.heads[0]);
            }
        }
    }

    void ItemHead()
    {
        string name = ss.name;
        ProjectManager.Instance.heads.RemoveAt(0);
        
        ProjectManager.Instance.heads.Add(Resources.Load<Item>(string.Format($"Head/{name}")));
        Debug.Log(transform.gameObject.name);
        Item itemHead = ProjectManager.Instance.heads[0];
        ProjectManager.Instance.heads[0] = ProjectManager.Instance.heads[1];
        ProjectManager.Instance.heads[1] = itemHead;
    }
}
