using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssenceData : MonoBehaviour
{
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
    public class Split
    {
        public List<Data> essence;
    }

    public Split split;
    [SerializeField] TextAsset essenceJson;

    void Start()
    {
        split = JsonUtility.FromJson<Split>(essenceJson.text);
    }

    public void OnEssenceData()
    {
        EssenceUI.Instance.SetData(split.essence[0]);
    }
}
