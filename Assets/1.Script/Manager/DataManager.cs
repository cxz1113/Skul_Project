using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public SkulData skulData;
    public EssenceData essenceData;
    public ItemData itemData;
    public PlayerData playerData;

    public string data;
    string path;
    void Awake()
    {
        Instance = this;
        path = Path.Combine(Application.streamingAssetsPath, "database.json");
        //path = Path.Combine(Application.dataPath, "Assets/database.json");

        LoadData();
        var data = FindObjectsOfType<DataManager>();
        if (data.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    public void SaveData()
    {
        // Data 저장 함수
        Player player = FindObjectOfType<Player>();
        if(player.isDead)
            ProjectManager.Instance.Data(100);
        else
            ProjectManager.Instance.Data(PlayerBasket.Instance.curHp);
        data = JsonUtility.ToJson(playerData.nowPlayerData, true);
        Debug.Log(data);
        File.WriteAllText(path, data);
    }

    public void LoadData()
    {
        // Database 저장 파일이 있으면 로드, 없으면 새로운 데이터 생성
        if(!File.Exists(path))
        {
            playerData.nowPlayerData = JsonUtility.FromJson<PlayerData.NowPlayerData>(playerData.playerJson.text);
        }
        else
        {
            data = File.ReadAllText(path);
            playerData.nowPlayerData = JsonUtility.FromJson<PlayerData.NowPlayerData>(data);
        }
    }
}
