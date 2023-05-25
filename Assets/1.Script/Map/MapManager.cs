using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public Player player;
    public Transform playerStart;
    public GameObject gate;
    public GameObject gold;
    public List<WayPoint> wayPoints = new List<WayPoint>();

    public bool isActive { get; set; }

    public bool isWay { get; set; }

    public bool isTown { get; set; }

    public bool isBoss { get; set; }

    void Awake() => Instance = this;

    void Update()
    {
        player = ProjectManager.Instance.player;
        // 골드 오브젝트 삭제뒤 웨이 포인트 스프라이트 활설화 및 웨이 포인트 눌렀을때 Scene이동
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
