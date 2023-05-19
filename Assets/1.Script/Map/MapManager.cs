using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool isTown { get; set; }

    public bool isBoss { get; set; }

    void Awake() => Instance = this;

    void Update()
    {
        if (!isActive && Instance.isWay)
        {
            Instance.isWay = false;

            foreach (var way in wayPoints)
            {
                way.GetComponent<SpriteAnimation>().SetSprite(way.GetComponent<WayPoint>().active, 0.1f);
                way.GetComponent<WayPoint>().collider2D.enabled = true;
                isTown = isBoss = false;
            }
        }
    }
}