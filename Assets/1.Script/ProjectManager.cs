using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager Instance;
    public PlayerDemo player;
    public PlayerData playerData;
    public PlayerUI ui;

    void Awake() => Instance = this;

    void Start()
    {
        PlayerSet();
        PlayerUISet();
        HPGage();
    }

    void PlayerSet()
    {
        playerData = FindObjectOfType<PlayerData>();
        player.curHp = playerData.nowPlayerData.playerdatajsons[0].curhp;
        player.maxHp = playerData.nowPlayerData.playerdatajsons[0].maxhp;
        player.head1 = playerData.nowPlayerData.playerdatajsons[0].head1;
        player.head2 = playerData.nowPlayerData.playerdatajsons[0].head2;
        player.item = playerData.nowPlayerData.playerdatajsons[0].item;
    }

    void PlayerUISet()
    {
        ui.curHpTxt.text = string.Format($"{player.HP}");
        ui.maxHpTxt.text = string.Format($"{player.maxHp}");
        ui.head1.sprite = player.heads[0].ss.headStatus1;
        ui.skill1.sprite = player.heads[0].ss.skill1;
        ui.skill2.sprite = player.heads[0].ss.Skill2;
        if (player.heads.Count >= 2)
        {
            ui.head2.sprite = player.heads[1].ss.headStatus2;
        }
    }

    void HPGage()
    {
        float hpEnergy = (player.curHp / player.maxHp) * 10f;
        hpEnergy = (float)System.Math.Truncate(hpEnergy);
        ui.hpGage.fillAmount = (hpEnergy / 10f);
    }
}
