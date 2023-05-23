using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance;
    public PlayerDemo playerDemo;
    public PlayerData json;
    void Awake() => Instance = this;

    private void Start()
    {
        json = FindObjectOfType<PlayerData>();
        PlayerDemo player = Instantiate(playerDemo, transform);
        player.curHp = json.nowPlayerData.playerdatajsons[0].curhp;
        player.maxHp = json.nowPlayerData.playerdatajsons[0].maxhp;
        player.head1 = json.nowPlayerData.playerdatajsons[0].head1;
        player.head2 = json.nowPlayerData.playerdatajsons[0].head2;
        player.item = json.nowPlayerData.playerdatajsons[0].item;
    }
}
