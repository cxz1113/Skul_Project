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

        [Serializable]
        public class playerDataHead
        {
            public string head;
        }

        [Serializable]
        public class playerDataEssence
        {
            public string essence;
        }

        [Serializable]
        public class playerDataItem1
        {
            public string item;
        }

        [Serializable]
        public class playerDataItem2
        {
            public string item;
        }

        public List<playerDataHead> playerhead = new List<playerDataHead>();
        public List<playerDataEssence> playeressence = new List<playerDataEssence>();
        public List<playerDataItem1> playeritem1 = new List<playerDataItem1>();
        public List<playerDataItem2> playeritem2 = new List<playerDataItem2>();
    }

    [Serializable]
    public class NowPlayerData
    {
        public List<PlayerDataJson> playerdatajsons = new List<PlayerDataJson>();
    }
}
