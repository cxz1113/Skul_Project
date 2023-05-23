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
        player.hp = json.nowPlayerData.playerdatajsons[0].hp;
        player.head1 = json.nowPlayerData.playerdatajsons[0].head1;
        player.head2 = json.nowPlayerData.playerdatajsons[0].head2;
        player.item = json.nowPlayerData.playerdatajsons[0].item;
    }
}
