using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [SerializeField] private TextAsset json;

    public NowPlayerData nowPlayerData;

    string path;

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

    void Awake()
    {
        Instance = this;
        //LoadData();
    }
    void Start()
    {
        path = Path.Combine(Application.dataPath, "database.json");
        nowPlayerData = JsonUtility.FromJson<NowPlayerData>(json.text);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayerData);
        File.WriteAllText(path,data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path);
        nowPlayerData = JsonUtility.FromJson<NowPlayerData>(data);
    }
}
