using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivity : MonoBehaviour
{
    public static PlayerActivity Instance;
    public PlayerDemo player;

    public bool isPush { get; set; }

    void Awake() => Instance = this;

    void Start()
    {
        
    }
}
