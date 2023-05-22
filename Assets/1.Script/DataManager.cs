using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    void Awake() => Instance = this;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
