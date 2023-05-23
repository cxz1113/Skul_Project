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

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
    }

    void Start()
    {
        PlayerSet();
        PlayerUISet();
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
        PlayerUI ui = PlayerUI.Instance;
        ui.curHpTxt.text = string.Format($"{player.HP}");
    }
}
