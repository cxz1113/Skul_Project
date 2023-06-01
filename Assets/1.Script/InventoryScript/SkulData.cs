using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkulData : MonoBehaviour
{
    
    [System.Serializable]
    public class Data
    {
        public string name;
        public string itemskul;
        public string tier;
        public string type;
        public string intro;
        public string detail;
        public string ability;
        public string skillname;
        public string skillspname;
        public string skillintrodetail;
        public string skilldetail;
    }

    [System.Serializable]
    public class SkulDataJson
    {
        public List<Data> skul;
    }

    [SerializeField] TextAsset skulJson;
    public SkulDataJson skulDataJson;

    void Start()
    {
        skulDataJson = JsonUtility.FromJson<SkulDataJson>(skulJson.text);
    }
}
