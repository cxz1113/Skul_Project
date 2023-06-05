using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkulData : MonoBehaviour
{
    public TextAsset skulJson;
    public SkulDataJson skulDataJson;
    
    [System.Serializable]
    public class Data
    {
        public string name;
        public string tier;
        public string type;
        public string intro;
        public string passive;
        public string ability;
        public string skillname1;
        public string skillname2;
        public string abilitydetail;
        public string skillname1detail;
        public string skillname2detail;
        public string cooltime;
        public string itemskul;
    }

    [System.Serializable]
    public class SkulDataJson
    {
        public List<Data> skul = new List<Data>();
    }

    void Start()
    {
        skulDataJson = JsonUtility.FromJson<SkulDataJson>(skulJson.text);
    }
}
