using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager Instance;
    public Player player;
    public PlayerBasket playerBasket;
    public Transform playerStart;
    public PlayerUI ui;
    public InvenManager inven;
    public List<Item> heads = new List<Item>();
    public List<Item> essences = new List<Item>();
    public List<Item> items = new List<Item>();
    public PlayerData playerData;
    public SkulData skulData;
    public ItemData itemData;
    public PlayerData.PlayerDataJson data;

    void Awake() =>Instance = this;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();

        data = playerData.nowPlayerData.playerdatajsons[0];
        Init(playerData.nowPlayerData.playerdatajsons[0].head1);
        PlayerSet();
        //HeadJson();
        PlayerUISet();
        InvenJson();
        HPGage();
    }

    void HeadFrametaSet()
    {
        string[] str = { data.head1, data.head2 };
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == string.Empty)
                break;
            else
            {
                heads.Add(Resources.Load<Item>($"Prefab/{str[i]}"));
                foreach (var item in heads)
                {
                    item.Init();
                }
            }
        }
    }
    void PlayerSet()
    {
        // 플레이어 Data를 playerBasket 전달
        playerBasket = FindObjectOfType<PlayerBasket>();
        HeadFrametaSet();
        playerBasket.curHp = data.curhp;
        playerBasket.maxHp = data.maxhp;
        playerBasket.item = data.item0;
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
        heads[0] = Resources.Load<Item>($"Prefab/{main}");
        heads[0].Init();
        if (heads[1] == null)
            return;
        
        heads[1] = Resources.Load<Item>($"Prefab/{sub}");
        heads[1].Init();
    }

    void Init(string main)
    {
        // Scene 이동시 플레이어 json 헤드 이름에 따라 그와 관련된 플레이어 생성
        player = Instantiate(Resources.Load<Player>(string.Format("Player/{0}",main)), playerStart);
    }
    public void HeadSwap()
    {
        // 플레이어 헤드프레임 스왑시 교체할 변수 및 함수
        Item headTemp = heads[0];
        heads[0] = heads[1];
        heads[1] = headTemp;
        player = FindObjectOfType<Player>();
        PlayerHeadSet(heads[0].name, heads[1].name);
        TextSet();
        ui.head1.sprite = heads[0].ss.headStatus1;
        ui.head2.sprite = heads[1].ss.headStatus2;
        SkillUI();
        //HeadJson();
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
        ui.skill1.sprite = heads[0].ss.skill1;
        if (heads[0].ss.Skill2 == null)
            ui.skill2.transform.parent.gameObject.SetActive(false);
        else
        {
            ui.skill2.transform.parent.gameObject.SetActive(true);
            ui.skill2.sprite = heads[0].ss.Skill2;
        }
    }

    public void HeadJson()
    {
        skulData = FindObjectOfType<SkulData>();
        skulData.skulDataJson = JsonUtility.FromJson<SkulData.SkulDataJson>(skulData.skulJson.text);

        int count = 0;
        while(count < skulData.skulDataJson.skul.Count)
        {
            if (heads[0].name != skulData.skulDataJson.skul[count].itemskul)
            {
                count++;
            }
            else
            {
                inven.itemSelect.skulJson = skulData.skulDataJson.skul[count];
                break;
            }
        }
    }

    public void InvenJson()
    {
        itemData = FindObjectOfType<ItemData>();
        itemData.itemDatajson = JsonUtility.FromJson<ItemData.ItemDatajson>(itemData.itemJson.text);

        /*int count = 0;
        while (count < itemData.itemDatajson.item.Count)
        {
            if (items[0].name != itemData.itemDatajson.item[count].)
            {
                count++;
            }
            else
            {
                inven.itemSelect.skulJson = skulData.skulDataJson.skul[count];
                break;
            }
        }*/
        ui.ImageSet();
        ItemSet();
        EssenceSet();
        inven.ItemBox(heads, essences, items);
    }

    void ItemSet()
    {
        itemData.itemDatajson = JsonUtility.FromJson<ItemData.ItemDatajson>(itemData.itemJson.text);

        string[] str = { data.item0, data.item1, data.item2, data.item3, data.item4, data.item5 };
        for(int i = 0; i < str.Length; i++)
        {
            if (str[i] == string.Empty)
                break;
            else
            {
                items.Add(Resources.Load<Item>($"Prefab/{str[i]}"));
                foreach (var item in items)
                {
                    item.Init();
                }
            }
        }
    }
    void EssenceSet()
    {
        string str = data.essence;
        if (str == string.Empty)
            return;

        essences.Add(Resources.Load<Item>($"Prefab/{str}"));
    }
    public void Data()
    {
        // 저장할 변수들 직렬화 함수
        //PlayerData.PlayerDataJson data = playerData.nowPlayerData.playerdatajsons[0];
        data.curhp = playerBasket.curHp;
        data.head1 = heads[0].name;
        data.head2 = heads[1].name;
        data.item0 = items[0].name;
    }
}
