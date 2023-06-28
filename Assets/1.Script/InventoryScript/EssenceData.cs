using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssenceData : MonoBehaviour
{
    public TextAsset essenceJson;
    public EssenceDatajson essenceDatajson;

    [System.Serializable]
    public class Data
    {
        public string name;
        public string tier;
        public int cooltime;
        public string intro;
        public string ability;
        public string detail;
        public string essencespname;
    }
    [System.Serializable]
    public class EssenceDatajson
    {
        public List<Data> essence = new List<Data>();
    }

    void Start()
    {
        essenceDatajson = JsonUtility.FromJson<EssenceDatajson>(essenceJson.text);
    }
}
