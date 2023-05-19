using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public PlayerDemo playerDemo;
    public TestJson json;
    private void Start()
    {
        json = FindObjectOfType<TestJson>();
        PlayerDemo player = Instantiate(playerDemo, transform);
        player.hp = json.playerData.player[0].hp;
        player.mp = json.playerData.player[0].mp;
    }
}
