using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string name;
        public string itempname;
        public string tier;
        public string intro;
        public string itemdetail;
        public string abillity1;
        public string abillity2;
        
    }

    [System.Serializable]
    public class ItemDatajson
    {
        public List<Data> item;
    }

    public ItemDatajson itemDatajson;
    [SerializeField] TextAsset itemJson;

    void Start()
    {
        itemDatajson = JsonUtility.FromJson<ItemDatajson>(itemJson.text);
    }

}
