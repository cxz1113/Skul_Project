using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager Instance;
    public Player player;
    public PlayerBasket playerBasket;
    public PlayerUI ui;
    public InvenManager inven;
    public PlayerData playerData;
    public PlayerData.PlayerDataJson data;
    public List<Item> heads = new List<Item>();
    public List<Item> essences = new List<Item>();
    public List<Item> items = new List<Item>();

    void Awake() => Instance = this;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();

        data = playerData.nowPlayerData.playerdatajsons[0];
        Init(playerData.nowPlayerData.playerdatajsons[0].head1);
        PlayerSet();
        PlayerUISet();
        InventorySet();
        HPGage();
    }

    void PlayerSet()
    {
        // 플레이어 Data를 playerBasket 전달
        playerBasket = FindObjectOfType<PlayerBasket>();
        HeadFrametaSet();
        playerBasket.curHp = data.curhp;
        playerBasket.maxHp = data.maxhp;
    }


    void PlayerUISet()
    {
        // 플레이어 인터페이스 UI set
        PlayerHeadSet(data.head1, data.head2);
        TextSet();
        ui.head1.sprite = heads[0].ss.headStatus1;
        if (heads.Count < 1)
            ui.head2.color = new Color(1f, 1f, 1f, 1f/255f);               
        else
            ui.head2.sprite = heads[1].ss.headStatus2;      
        SkillUI();
    }

    void HPGage()
    {
        float hpEnergy = (playerBasket.curHp / playerBasket.maxHp) * 10f;
        hpEnergy = (float)System.Math.Truncate(hpEnergy);
        ui.hpGage.fillAmount = (hpEnergy / 10f);
    }

    public void PlayerHeadSet(string main, string sub)
    {
        // 플레이어 헤드프리펩 설정 
        heads[0] = Resources.Load<Item>($"Head/{main}");
        heads[0].Init();
        if (heads[1] == null)
            return;
        
        heads[1] = Resources.Load<Item>($"Head/{sub}");
        heads[1].Init();
    }

    void Init(string main)
    {
        // Scene 이동시 플레이어 json 헤드 이름에 따라 그와 관련된 플레이어 생성
        player = Instantiate(Resources.Load<Player>(string.Format($"Player/{main}")));
    }

    public void HeadSwap()
    {
        // 플레이어 헤드프레임 스왑시 교체할 변수 및 함수
        InvenHeadChage(heads, InvenManager.Instance.itemBox, ui.imagesItem); 
        player = FindObjectOfType<Player>();
        PlayerHeadSet(heads[0].name, heads[1].name);
        TextSet();
        ui.head1.sprite = heads[0].ss.headStatus1;
        ui.head2.sprite = heads[1].ss.headStatus2;
        SkillUI();
        HPGage();
    }

    public void ItemHeadChange()
    {
        player = FindObjectOfType<Player>();
        inven.ItemBox(heads, essences, items);
        PlayerHeadSet(heads[0].name, heads[1].name);
        TextSet();
        ui.head1.sprite = heads[0].ss.headStatus1;
        ui.head2.sprite = heads[1].ss.headStatus2;
        SkillUI();
        HPGage();
    }

    void TextSet()
    {
        ui.curHpTxt.text = $"{playerBasket.HP}";
        ui.maxHpTxt.text = $"{playerBasket.maxHp}";
    }

    void SkillUI()
    {
        // 플레이어 인터페이스 Skill UI set
        ui.skill1Sprite.sprite = heads[0].ss.skill1;
        if (heads[0].ss.Skill2 == null)
            ui.skill2Sprite.transform.parent.gameObject.SetActive(false);
        else
        {
            ui.skill2Sprite.transform.parent.gameObject.SetActive(true);
            ui.skill2Sprite.sprite = heads[0].ss.Skill2;
        }
    }

    public void InventorySet()
    {
        ui.ImageSet();
        ItemSet();
        EssenceSet();
        inven.ItemBox(heads, essences, items);
    }

    void HeadFrametaSet()
    {
        // PlayerDataJson Head Set
        string[] str = { data.head1, data.head2 };
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == string.Empty)
                break;
            else
            {
                heads.Add(Resources.Load<Item>($"Head/{str[i]}"));
                foreach (var item in heads)
                {
                    item.Init();
                }
            }
        }
    }

    void EssenceSet()
    {
        // PlayerDataJson Essence Set
        string str = data.essence;
        if (str == string.Empty)
            return;

        essences.Add(Resources.Load<Item>($"Prefab/{str}"));
    }

    void ItemSet()
    {
        // PlayerDataJson Item Set
        string[] str = { data.item0, data.item1, data.item2, data.item3, data.item4, data.item5 };

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == string.Empty)
                break;
            else
            {
                items.Add(Resources.Load<Item>($"Item/{str[i]}"));
                foreach (var item in items)
                {
                    item.Init();
                }
            }
        }
    }

 
    void InvenHeadChage(List<Item> item, Item[][] itemBoxes, Image[][] images)
    {
        // In Game UI HeadChange
        Item headTemp = item[0];
        item[0] = item[1];
        item[1] = headTemp;

        // ItemBox HeadChange
        Item BoxTemp = itemBoxes[0][0];
        itemBoxes[0][0] = itemBoxes[0][1];
        itemBoxes[0][1] = BoxTemp;

        // ImageBox HeadChange

        Sprite imageTemp = images[0][0].sprite;
        images[0][0].sprite = images[0][1].sprite;
        images[0][1].sprite = imageTemp;
    }

    public void Data()
    {
        // 저장할 변수들 직렬화 함수
        //PlayerData.PlayerDataJson data = playerData.nowPlayerData.playerdatajsons[0];
        data.curhp = playerBasket.curHp;
       // data.head1 = heads[0].name;
        //data.head2 = heads[1].name;
        DataSet(heads);
        //DataSet(essences);
        //data.essence = essences[0].name;
        //DataSet(items);

    }

    void DataSet(List<Item> iList)
    {
        string[] dataHead = { data.head1, data.head2 };
        string[] dataEssence = { data.essence };
        string[] dataItem = { data.item0, data.item1, data.item2, data.item3, data.item4, data.item5 };

        if (iList == heads)
            DataTest(iList, dataHead);
        else if (iList == essences)
            DataTest(iList, dataEssence);
        else if (iList == items)
            DataTest(iList, dataItem);

    }

    public void DataTest(List<Item> itemType, string[] strs)
    {
        if (itemType.Count == 0)
            return;
        for (int i = 0; i < itemType.Count; i++)
        {
            strs[i] = itemType[i].name;
        }
    }
}
