using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public Player player;
    public Transform playerStart;
    public GameObject gate;
    public GameObject goldParent;
    public GameObject coinParent;
    public GameObject coin;
    public Item head;
    public Item dropHead;
    public Transform coinTrans;
    public Transform headTrans;
    public string itemName;
    public int itemCount = 0;
    public List<WayPoint> wayPoints = new List<WayPoint>();
    int count = 24;

    public bool isActive { get; set; }

    public bool isWay { get; set; }

    public bool isTown { get; set; }

    public bool isBoss { get; set; }

    public bool isGold { get; set; }
    public bool isHead { get; set; }

    void Awake() => Instance = this;

    void Update()
    {
        player = ProjectManager.Instance.player;
        // ��� ������Ʈ ������ ���� ����Ʈ ��������Ʈ Ȱ��ȭ �� ���� ����Ʈ �������� Scene�̵�
        if (!isActive && isWay && isGold)
        {
            isGold = false;
            CoinDrop();
            Instance.isWay = false;

            WayPoint();
        }
        else if(!isActive && isWay && isHead)
        {
            isHead = false;
            HeadDrop();
            Instance.isWay = false;

            WayPoint();
        }
    }

    void WayPoint()
    {
        foreach (var way in wayPoints)
        {
            way.GetComponent<SpriteAnimation>().SetSprite(way.GetComponent<WayPoint>().active, 0.1f);
            way.GetComponent<WayPoint>().collider2D.enabled = true;
            isTown = isBoss = false;
        }
    }
    void CoinDrop()
    {
        for(int i = 0; i < 80; i++)
        {
            Instantiate(coin, coinTrans);
        }
    }

    void HeadDrop()
    {
        head = ItemName();
        head.Init();
        Instantiate(head, headTrans);
    }

    public void ItemDrop()
    {
        if (dropHead != null)
        {
            head = Resources.Load<Item>(string.Format($"Head/{ProjectManager.Instance.heads[0].name}"));
            Instantiate(head, headTrans);
        }
        else
        {
            dropHead = Resources.Load<Item>(string.Format($"Head/{ProjectManager.Instance.heads[0].name}"));
            Instantiate(dropHead, headTrans);
        }
    }

    Item ItemName()
    {
        int count = 0;
        PlayerBasket basket = FindObjectOfType<PlayerBasket>();
        Item item = FindObjectOfType<Item>();
        while (count < basket.heads.Count)
        {
            if (!ProjectManager.Instance.heads.Contains(basket.heads[count]))
            {
                item = basket.heads[count];
                break;
            }
            else
                count++;
        }
        return item;
    }
}
