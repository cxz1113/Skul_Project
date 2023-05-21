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
        public string tier;
        public int tierindex;
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
    public class Split
    {
        public List<Data> skul;
    }

    [SerializeField] TextAsset skulJson;
    public Split split;
    public ToggleGroup toggleGroup;



    void Start()
    {
        split = JsonUtility.FromJson<Split>(skulJson.text);
    }

    public void OnSkulData()
    {
        SkulUI.Instance.SetData(split.skul[0]);
    }
}
