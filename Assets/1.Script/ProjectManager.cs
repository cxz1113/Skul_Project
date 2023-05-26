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
    public PlayerData playerData;
    public SkulData skulData;
    public PlayerUI ui;
    public List<Head> heads = new List<Head>();
    PlayerData.PlayerDataJson data;
    void Awake() => Instance = this;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();

        data = playerData.nowPlayerData.playerdatajsons[0];
        Init(playerData.nowPlayerData.playerdatajsons[0].head1);
        HeadFrame(data.head1, data.head2);
        PlayerSet();
        HeadJson();
        PlayerUISet();
        HPGage();
    }

    void HeadFrame(string main, string sub)
    {
        heads.Add(Resources.Load<Head>(string.Format("Prefab/{0}", main)));
        heads.Add(Resources.Load<Head>(string.Format("Prefab/{0}", sub)));
    }
    void PlayerSet()
    {
        playerBasket = FindObjectOfType<PlayerBasket>();

        playerBasket.curHp = data.curhp;
        playerBasket.maxHp = data.maxhp;
        playerBasket.item = data.item;
    }

    void PlayerUISet()
    {
        PlayerHeadSet(data.head1, data.head2);
        ui.curHpTxt.text = string.Format($"{playerBasket.HP}");
        ui.maxHpTxt.text = string.Format($"{playerBasket.maxHp}");
        ui.head1.sprite = heads[0].ss.headStatus1;
        if (heads[1] == null)
        {
            ui.head2.color = new Color(1f, 1f, 1f, 1f/255f);
        }
        else
        {
            ui.head2.sprite = heads[1].ss.headStatus2;
        }
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
        heads[0] = Resources.Load<Head>(string.Format("Prefab/{0}", main));
        heads[0].Init();
        if (heads[1] == null)
            return;
        
        heads[1] = Resources.Load<Head>(string.Format("Prefab/{0}", sub));
        heads[1].Init();
    }

    void Init(string main)
    {
        player = Instantiate(Resources.Load<Player>(string.Format("Player/{0}",main)), playerStart);
    }
    public void HeadSwap()
    {
        Head headTemp = heads[0];
        heads[0] = heads[1];
        heads[1] = headTemp;
        player = FindObjectOfType<Player>();
        PlayerHeadSet(heads[0].name, heads[1].name);
        ui.curHpTxt.text = string.Format($"{playerBasket.HP}");
        ui.maxHpTxt.text = string.Format($"{playerBasket.maxHp}");
        ui.head1.sprite = heads[0].ss.headStatus1;
        ui.head2.sprite = heads[1].ss.headStatus2;
        SkillUI();
        HeadJson();
        HPGage();
    }

    void SkillUI()
    {
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
        int count = 0;
        while(count < skulData.split.skul.Count)
        {
            if (heads[0].name != skulData.split.skul[count].itemskul)
            {
                count++;
            }
            else
            {
                PlayerBasket.Instance.skul = skulData.split.skul[count].itemskul;
                break;
            }
        }
    }
    public void Data()
    {
        PlayerData.PlayerDataJson data = playerData.nowPlayerData.playerdatajsons[0];
        data.curhp = playerBasket.curHp;
        data.head1 = heads[0].name;
        data.head2 = heads[1].name;
    }
}
