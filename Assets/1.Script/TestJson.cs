using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TestJson : MonoBehaviour
{
    [SerializeField] TextAsset json;

    public PlayerTest playerData = new PlayerTest();

    [Serializable]
    public class PlayerData
    {
        public int hp;
        public int head1;
        public int head2;
        public int item;
    } 

    [Serializable]
    public class ItemData
    {
        public int head;
    }

    [Serializable]
    public class SkulData
    {
        public int abc;
    }
    [Serializable]
    public class PlayerTest
    {
        public List<PlayerData> player = new List<PlayerData>(); 
    }

    void Start()
    {
        playerData = JsonUtility.FromJson<PlayerTest>(json.text);
    }
}
