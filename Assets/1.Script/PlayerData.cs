using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private TextAsset json;

    public NowPlayerData nowPlayerData;

    [Serializable]
    public class PlayerDataJson
    {
        public int hp;
        public int head1;
        public int head2;
        public int item;
    }

    [Serializable]
    public class SkulDataJson
    {

    }

    [Serializable]
    public class ItemDataJson
    {

    }

    [Serializable]
    public class NowPlayerData
    {
        public List<PlayerDataJson> playerdatajsons = new List<PlayerDataJson>();
    }

    void Start()
    {
        nowPlayerData = JsonUtility.FromJson<NowPlayerData>(json.text);
    }
}
