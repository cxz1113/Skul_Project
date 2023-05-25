using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class PlayerData : MonoBehaviour
{
    public TextAsset playerJson;

    public NowPlayerData nowPlayerData;

    [Serializable]
    public class PlayerDataJson
    {
        public float curhp;
        public float maxhp;
        public int head1;
        public int head2;
        public int item;
    }

    [Serializable]
    public class NowPlayerData
    {
        public List<PlayerDataJson> playerdatajsons = new List<PlayerDataJson>();
    }
}
