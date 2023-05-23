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
    public Queue<string> saveData = new Queue<string>();
    public string data;
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
    }
    void Start()
    {
        if(saveData == null)
        {
            nowPlayerData = JsonUtility.FromJson<NowPlayerData>(json.text);
        }
        else if(saveData != null)
        {
            path = Path.Combine(Application.dataPath, "database.json");
            LoadData();
        }
    }

    public void SaveData()
    {
        data = JsonUtility.ToJson(nowPlayerData);
        saveData.Enqueue(data);
        Debug.Log(saveData);
        File.WriteAllText(path,data);
    }

    public void LoadData()
    {
        data = File.ReadAllText(path);
        nowPlayerData = JsonUtility.FromJson<NowPlayerData>(data);
    }

    public void SaveDataDequeue()
    {
        string sd = data;
    }
}
