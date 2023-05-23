using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivity : MonoBehaviour
{
    public static PlayerActivity Instance;
    public PlayerDemo player;
    public PlayerData playerData;

    public bool isPush { get; set; }

    void Awake() => Instance = this;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        player.curHp = playerData.nowPlayerData.playerdatajsons[0].curhp;
        player.maxHp = playerData.nowPlayerData.playerdatajsons[0].maxhp;
        player.head1 = playerData.nowPlayerData.playerdatajsons[0].head1;
        player.head2 = playerData.nowPlayerData.playerdatajsons[0].head2;
        player.item = playerData.nowPlayerData.playerdatajsons[0].item;
    }

    void Update()
    {

    }
}
