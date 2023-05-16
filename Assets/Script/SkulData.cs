using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulData : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string name;
        public string tier;
        public string type;
        public string intro;
        public string detail;
        public string ability;
        public string skillname;
        public string skillspname;
    }

    [System.Serializable]
    public class Split
    {
        public List<Data> skul;
    }

    public Split split;
    [SerializeField] TextAsset skulJson;
    void Start()
    {
        split = JsonUtility.FromJson<Split>(skulJson.text);
    }

}
