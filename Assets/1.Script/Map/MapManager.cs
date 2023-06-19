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
    public Item seconDrop;
    public Transform coinTrans;
    public Transform headTrans;
    public string itemName;
    public int itemCount = 0;
    public List<WayPoint> wayPoints = new List<WayPoint>();

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
        // 골드 오브젝트 삭제뒤 웨이 포인트 스프라이트 활설화 및 웨이 포인트 눌렀을때 Scene이동
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
        head = Resources.Load<Item>($"Head/{ItemName()}");
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

    public string ItemName()
    {
        int count = 0;
        string[] strs = { "Sword","LittleBorn", "Wolf"  };
        while(count < strs.Length)
        {
            if (strs[count] != ProjectManager.Instance.heads[count].name)
            {
                itemName = strs[count];
                break;
            }
            else
                count++;
        }
        return itemName;
    }
}
