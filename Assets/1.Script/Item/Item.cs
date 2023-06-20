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
    public Item itobj;
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
    public Item item;
    public Item dropItem;
    public string pName;
    public bool isHead { get; set; }
    public bool isItem { get; set; }
    public abstract void Init();

    void Update()
    {
        //item = FindObjectOfType<MapManager>().head;
        //dropItem = FindObjectOfType<MapManager>().dropHead;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && it == ItemType.Head)
        {
            if (ProjectManager.Instance.player.isPush)
            {
                ProjectManager.Instance.player.isPush = false;
                ItemHead();
                PlayerChange();
                Destroy(ss.obj);
            }
        }

    }

    void ItemHead()
    {
        MapManager map = FindObjectOfType<MapManager>();
        map.ItemDrop();
        if(map.itemCount == 0)
        {
            map.head.Init();
            HeadChange(map.head);
            map.itemCount++;
        }
        else if(map.itemCount != 0)
        {
            map.dropHead.Init();
            Debug.Log(map.dropHead);
            HeadChange(map.dropHead);
            map.itemCount = 0;
            map.dropHead = null;
        }
    }

    void HeadChange(Item item)
    {
        ProjectManager manager = FindObjectOfType<ProjectManager>();
        manager.heads.Add(item);
        manager.heads.RemoveAt(0);
        Item itemHead = manager.heads[0];
        manager.heads[0] = manager.heads[1];
        manager.heads[1] = itemHead;
    }

    void PlayerChange()
    {
        MapManager map = FindObjectOfType<MapManager>();
        if (map.itemCount == 0)
            PlayerFind();
        else if (map.itemCount == 1)
            PlayerFind();
    }

    void PlayerFind()
    {
        Player player = FindObjectOfType<Player>();
        int count = 0;
        while(count < player.players.Count)
        {
            if (ProjectManager.Instance.heads[0].name != player.players[count].name)
                count++;
            else
            {
                player.ItemSwitch(player.players[count]);
                break;
            }
        }
    }

    Item ItemFind()
    {
        PlayerBasket basket = FindObjectOfType<PlayerBasket>();
        int count = 0;
        while(count < basket.heads.Count)
        {
            if (ss.obj.name != basket.heads[count].name)
                count++;
            else
            {
                item = basket.heads[count];
                Debug.Log(basket.heads[count]);
                break;
            }
        }
        Debug.Log(item);
        return item;
    }
    GameObject Find(Item desItem)
    {
        Player player = FindObjectOfType<Player>();
        GameObject desPlayer = player.gameObject;
        int count = 0;
        while(count < player.players.Count)
        {
            if (desItem.name != player.players[count].name)
                count++;
            else
            {
                desPlayer = player.players[count].gameObject;
                break;
            }
        }
        return desPlayer;
    }
}
