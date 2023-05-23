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
    public List<string> saveDatas = new List<string>();
    
    public string data;
    string path;

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
    }
    void Start()
    {
        path = Path.Combine(Application.dataPath, "database.json");
        LoadData();
    }

    public void SaveData()
    {
        data = JsonUtility.ToJson(nowPlayerData, true);
        File.WriteAllText(path, data);
    }

    public void LoadData()
    {
        if(!File.Exists(path))
        {
            nowPlayerData = JsonUtility.FromJson<NowPlayerData>(json.text);
        }
        else
        {
            data = File.ReadAllText(path);
            nowPlayerData = JsonUtility.FromJson<NowPlayerData>(data);
        }
    }
}
