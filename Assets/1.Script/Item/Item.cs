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
                //PlayerChange(dropItem);
                //Destroy(ss.obj);
            }
        }

    }

    void ItemHead()
    {
        MapManager map = FindObjectOfType<MapManager>();
        if(map.dropHead != null)
        {
            map.head = Instantiate(Resources.Load<Item>(string.Format($"Head/{ProjectManager.Instance.heads[0].name}")), MapManager.Instance.headTrans);
        }
        else
        {
            map.dropHead = Instantiate(Resources.Load<Item>(string.Format($"Head/{ProjectManager.Instance.heads[0].name}")), MapManager.Instance.headTrans);
        }
        if(map.itemCount == 0)
        {
            map.head.Init();
            ProjectManager.Instance.heads.Add(map.head);
            ProjectManager.Instance.heads.RemoveAt(0);
            Item itemHead = ProjectManager.Instance.heads[0];
            ProjectManager.Instance.heads[0] = ProjectManager.Instance.heads[1];
            ProjectManager.Instance.heads[1] = itemHead;
            map.itemCount++;
            //Destroy(ss.obj);
        }
        else if(map.itemCount != 0)
        {
            map.dropHead.Init();
            Debug.Log(map.dropHead);

            ProjectManager.Instance.heads.Add(map.dropHead);
            ProjectManager.Instance.heads.RemoveAt(0);
            Item itemHead = ProjectManager.Instance.heads[0];
            ProjectManager.Instance.heads[0] = ProjectManager.Instance.heads[1];
            ProjectManager.Instance.heads[1] = itemHead;
            map.itemCount = 0;
            map.dropHead = null;
        }
    }

    void PlayerChange(Item item)
    {
        Player player = FindObjectOfType<Player>();
        int count = 0;
        while(count < player.players.Count)
        {
            if (ProjectManager.Instance.heads[0].name != player.players[count].name)
                count++;
            else
            {
                player = Instantiate(player.players[count], player.transform);
                player.transform.SetParent(null);
                player.SwitchInit(player);
                Destroy(Find(item));
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
