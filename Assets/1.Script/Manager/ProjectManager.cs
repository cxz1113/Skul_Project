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
    public List<Item> items1 = new List<Item>();
    public List<Item> items2 = new List<Item>();
    public bool isPasue = false;

    void Awake() => Instance = this;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();

        data = playerData.nowPlayerData.playerdatajsons[0];
        Init(data.playerhead[0].head);
        PlayerSet();
        PlayerUISet();
        InventorySet();
        HPGage();
    }

    void PlayerSet()
    {
        // �÷��̾� Data�� playerBasket ����
        playerBasket = FindObjectOfType<PlayerBasket>();
        ItemSet();
        playerBasket.curHp = data.curhp;
        playerBasket.maxHp = data.maxhp;
    }
    void Init(string main)
    {
        // Scene �̵��� �÷��̾� json ��� �̸��� ���� �׿� ���õ� �÷��̾� ����
        player = Instantiate(Resources.Load<Player>(string.Format($"Player/{main}")));
    }

    void PlayerUISet()
    {
        // �÷��̾� �������̽� UI set
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

    public void HeadSwap()
    {
        // �÷��̾� ��������� ���ҽ� ��ü�� ���� �� �Լ�
        InvenHeadChage(heads, InvenManager.Instance.itemBox, ui.imagesItem); 
        player = FindObjectOfType<Player>();
        TextSet();
        ui.head1.sprite = heads[0].ss.headStatus1;
        ui.head2.sprite = heads[1].ss.headStatus2;
        SkillUI();
    }

    public void ItemHeadChange()
    {
        // �÷��̾ ���ο� Head�� �Ծ��� �� ��ü�� ���� �� �Լ�
        player = FindObjectOfType<Player>();
        inven.ItemBox(heads, essences, items1, items2);
        TextSet();
        ui.head1.sprite = heads[0].ss.headStatus1;
        ui.head2.sprite = heads[1].ss.headStatus2;
        SkillUI();
    }

    void TextSet()
    {
        ui.curHpTxt.text = $"{playerBasket.HP}";
        ui.maxHpTxt.text = $"{playerBasket.maxHp}";
    }

    void SkillUI()
    {
        // �÷��̾� �������̽� Skill UI set
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
        inven.ItemBox(heads, essences, items1, items2);
    }

    void ItemSet()
    {
        // PlayerDataJson Head, Essence, Item Set 
        string[] strHead = { data.playerhead[0].head, data.playerhead[1].head };
        string[] strEssence = { data.playeressence[0].essence };
        string[] strItem1 = { data.playeritem1[0].item, data.playeritem1[1].item, data.playeritem1[2].item };
        string[] strItem = { data.playeritem2[0].item, data.playeritem2[1].item, data.playeritem2[2].item };

        ItemSetting(strHead, heads);
        ItemSetting(strEssence, essences);
        ItemSetting(strItem1, items1);
        ItemSetting(strItem, items2);
    }
    void ItemSetting(string[] strs, List<Item> item)
    {
        // ResouceLoad�� �̿��� ������ input�ڵ�
        for(int i = 0; i < strs.Length; i++)
        {
            if (strs[i] == string.Empty)
                break;
            else
            {
                if(item == heads)
                    item.Add(Resources.Load<Item>($"Head/{strs[i]}"));
                else if(item == essences)
                    item.Add(Resources.Load<Item>($"Essence/{strs[i]}"));
                else if (item == items1)
                    item.Add(Resources.Load<Item>($"Item/{strs[i]}"));
                else if(item == items2)
                    item.Add(Resources.Load<Item>($"Item/{strs[i]}"));

                foreach (var obj in item)
                    obj.Init();
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

    public void Data(float hp)
    {
        // ������ ������ ����ȭ �Լ�
        data.curhp = hp;
        DataTest(heads);
        DataTest(essences);
        DataTest(items1);
        DataTest(items2);
    }

    public void DataTest(List<Item> itemType)
    {
        // ������ ������ ����ȭ �ڵ�
        if (itemType.Count == 0)
            return;
        for (int i = 0; i < itemType.Count; i++)
        {
            if(itemType == heads)
                data.playerhead[i].head = itemType[i].name;
            else if(itemType == essences)
                data.playeressence[i].essence = itemType[i].name;
            else if(itemType == items1)
                data.playeritem1[i].item = itemType[i].name;
            else if(itemType == items2)
                data.playeritem2[i].item = itemType[i].name;
        }
    }
}
