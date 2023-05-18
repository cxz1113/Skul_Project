using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public PlayerDemo player;
    public GameObject gate;
    public GameObject gold;
    public List<WayPoint> wayPoints = new List<WayPoint>();
    public bool isActive { get; set; }
    public bool isPush { get; set; }

    public bool isWay { get; set; }

    void Awake() => Instance = this;

    void Update()
    {
        
    }
}
