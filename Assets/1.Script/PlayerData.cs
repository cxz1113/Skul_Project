using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public TextAsset playerJson;
    public NowPlayerData nowPlayerData;

    [Serializable]
    public class PlayerDataJson
    {
        public float curhp;
        public float maxhp;
        public string head1;
        public string head2;
        public string essence;
        public string item0;
        public string item1;
        public string item2;
        public string item3;
        public string item4;
        public string item5;
    }

    [Serializable]
    public class NowPlayerData
    {
        public List<PlayerDataJson> playerdatajsons = new List<PlayerDataJson>();
    }
}
