using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string name;
        public string itemspname;
        public string intro;
        public string itemdetail;
        public string tier;
        public string value1;
        public string value2;
        public string abillity1;
        public string abillity2;
        public int physical;
        public int magic;
        public int defence;
    }

    [System.Serializable]
    public class Split
    {
        public List<Data> item;
    }

    public Split split;
    [SerializeField] TextAsset itemJson;
    void Start()
    {
        split = JsonUtility.FromJson<Split>(itemJson.text);
    }

    public void OnItemData()
    {
        ItemUI.Instance.SetData(split.item[0]);
    }
}
